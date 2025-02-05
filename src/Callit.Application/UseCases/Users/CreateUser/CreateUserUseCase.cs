using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Callit.Communication.Requests;
using Callit.Communication.Responses;
using Callit.Domain.Entities;
using Callit.Domain.Repositories;
using Callit.Domain.Repositories.Users;
using Callit.Domain.Security.BCrypt;
using Callit.Exception.ExceptionBase;

namespace Callit.Application.UseCases.Users.CreateUser;

public class CreateUserUseCase : ICreateUserUseCase
{
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IPasswordEncripter _passwordEncripter;
	
	public CreateUserUseCase(
		IUserRepository userRepository, 
		IMapper mapper, IUnitOfWork unitOfWork, 
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
		Validate(request);
		
		var user = _mapper.Map<User>(request);
		user.Password = _passwordEncripter.Encrypt(request.Password);

		await _userRepository.CreateUser(user);
		await _unitOfWork.Commit();
		
		return _mapper.Map<ResponseCreatedUserJson>(user);
	}


	private void Validate(RequestUserJson request)
	{
		var validator = new CreateUserValidator();

		var result = validator.Validate(request);

		if (result.IsValid == false)
		{
			var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

			throw new ErrorOnValidationException(errorMessages);
		}
	}
}