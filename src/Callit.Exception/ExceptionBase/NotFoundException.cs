using System.Net;

namespace Callit.Exception.ExceptionBase;

public class NotFoundException : CallitException
{
  public override int StatusCode => (int)HttpStatusCode.NotFound;

  public NotFoundException(string message)
    : base(message) { }

  public override List<string> GetErrors()
  {
    return [Message];
  }
}
