using Callit.Domain.Entities;

namespace Callit.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
  string GenerateAccessToken(User user);
}
