﻿using Callit.Domain.Security.Tokens;

namespace Callit.Api.Token;

public class HttpContextTokenValue : ITokenProvider
{
  private readonly IHttpContextAccessor _httpContextAccessor;

  public HttpContextTokenValue(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  public string TokenOnRequest()
  {
    var authorization = _httpContextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

    return authorization["Bearer ".Length..].Trim();
  }
}
