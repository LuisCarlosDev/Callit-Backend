using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Callit.Communication.Requests;
using Callit.Communication.Responses;
using Callit.Domain.Entities;
using Callit.Domain.Repositories;
using Callit.Domain.Repositories.Users;
using Callit.Domain.Security.BCrypt;
using Callit.Domain.Security.Tokens;
using Callit.Exception.ExceptionBase;
using FluentValidation.Results;

namespace Callit.Application.UseCases.Users.CreateUser;

public class CreateUserUseCase : ICreateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _tokenGenerator;

    public CreateUserUseCase(
        IUserRepository userRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IPasswordEncripter passwordEncripter,
        IAccessTokenGenerator tokenGenerator
    )
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _passwordEncripter = passwordEncripter;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<ResponseCreatedUserJson> Execute(RequestUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<User>(request);
        user.Password = _passwordEncripter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();

        await _userRepository.CreateUser(user);
        await _unitOfWork.Commit();

        return new ResponseCreatedUserJson
        {
            Name = user.Name,
            Token = _tokenGenerator.GenerateAccessToken(user),
        };
    }

    private async Task Validate(RequestUserJson request)
    {
        var result = new CreateUserValidator().Validate(request);

        var userAlreadyExists = await _userRepository.UserAlreadyExists(request.Email);

        if (userAlreadyExists)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, "Email already exists"));
        }

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
