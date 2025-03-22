using Callit.Communication.Requests;
using Callit.Communication.Responses;

namespace Callit.Application.UseCases.SingIn.SignInUser;

public interface ISignInUserUseCase
{
    Task<ResponseSignInJson> Execute(RequestSignInJson request);
}
