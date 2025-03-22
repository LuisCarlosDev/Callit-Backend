using Callit.Communication.Requests;
using Callit.Communication.Responses;

namespace Callit.Application.UseCases.Users.CreateUser;

public interface ICreateUserUseCase
{
    Task<ResponseCreatedUserJson> Execute(RequestUserJson request);
}
