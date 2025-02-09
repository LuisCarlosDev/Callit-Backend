using System.Net;

namespace Callit.Exception.ExceptionBase;

public class InvalidSignInException : CallitException
{
	public InvalidSignInException() : base("Email or password is incorrect.")
	{
	}

	public override int StatusCode => (int)HttpStatusCode.Unauthorized;

	public override List<string> GetErrors()
	{
		return [Message];
	}
}