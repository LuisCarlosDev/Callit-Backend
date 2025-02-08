using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Callit.Communication.Requests;
using Callit.Communication.Responses;
using Callit.Domain.Entities;
using Callit.Domain.Repositories;
using Callit.Domain.Repositories.Users;
using Callit.Domain.Security.BCrypt;
using Callit.Exception.ExceptionBase;
using FluentValidation.Results;

namespace Callit.Application.UseCases.Users.CreateUser;

public class CreateUserUseCase : ICreateUserUseCase
{
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IPasswordEncripter _passwordEncripter;
	
	public CreateUserUseCase(
		IUserRepository userRepository, 
		IMapper mapper, 
		IUnitOfWork unitOfWork, 
		IPasswordEncripter passwordEncripter
		)
	{
		_userRepository = userRepository;
		_mapper = mapper;
		_unitOfWork = unitOfWork;
		_passwordEncripter = passwordEncripter;
	}
	
	public async Task<ResponseCreatedUserJson> Execute(RequestUserJson request)
	{
		await Validate(request);
		
		var user = _mapper.Map<User>(request);
		user.Password = _passwordEncripter.Encrypt(request.Password);
		user.UserIdentifier = Guid.Empty;

		await _userRepository.CreateUser(user);
		await _unitOfWork.Commit();

		return new ResponseCreatedUserJson
		{
			Name = user.Name,
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