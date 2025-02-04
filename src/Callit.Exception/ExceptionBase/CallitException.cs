namespace Callit.Exception.ExceptionBase;

public abstract class CallitException : SystemException
{
	protected CallitException(string message) : base(message)
	{
		
	}
	
	public abstract int StatusCode { get; }

	public abstract List<string> GetErrors();
}