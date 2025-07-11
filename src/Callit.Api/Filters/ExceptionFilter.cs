using Callit.Communication.Responses;
using Callit.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Callit.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
  public void OnException(ExceptionContext context)
  {
    if (context.Exception is CallitException)
    {
      HandleProjectException(context);
    }
    else
    {
      ThrowUnknownError(context);
    }
  }

  private void HandleProjectException(ExceptionContext context)
  {
    var callitException = (CallitException)context.Exception;
    var errorResponse = new ResponseErrorJson(callitException.GetErrors());

    context.HttpContext.Response.StatusCode = callitException.StatusCode;
    context.Result = new ObjectResult(errorResponse);
  }

  private void ThrowUnknownError(ExceptionContext context)
  {
    var errorResponse = new ResponseErrorJson("Unknown Error");

    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
    context.Result = new ObjectResult(context.Exception.Message);
  }
}
