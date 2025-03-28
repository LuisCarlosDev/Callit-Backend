using Callit.Communication.Requests;
using Callit.Communication.Responses;
using Callit.Domain.Repositories.Users;
using Callit.Domain.Security.BCrypt;
using Callit.Domain.Security.Tokens;
using Callit.Exception.ExceptionBase;

namespace Callit.Application.UseCases.SingIn.SignInUser;

public class SignInUserUseCase : ISignInUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public SignInUserUseCase(
        IUserRepository userRepository,
        IPasswordEncripter passwordEncripter,
        IAccessTokenGenerator accessTokenGenerator
    )
    {
        _userRepository = userRepository;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseSignInJson> Execute(RequestSignInJson request)
    {
        var user = await _userRepository.GetUserByEmail(request.Email);

        if (user is null)
        {
            throw new InvalidSignInException();
        }

        var passwordMatch = _passwordEncripter.Verify(request.Password, user.Password);

        if (passwordMatch == false)
        {
            throw new InvalidSignInException();
        }

        return new ResponseSignInJson
        {
            User = new UserLogged
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
            },
            Token = _accessTokenGenerator.GenerateAccessToken(user),
        };
    }
}
