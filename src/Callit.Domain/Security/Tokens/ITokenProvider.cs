namespace Callit.Domain.Security.Tokens;

public interface ITokenProvider
{
	string TokenOnRequest();
}