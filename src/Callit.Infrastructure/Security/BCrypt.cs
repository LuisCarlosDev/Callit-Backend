using Callit.Domain.Security.BCrypt;
using BC = BCrypt.Net.BCrypt;

namespace Callit.Infrastructure.Security;

public class BCrypt : IPasswordEncripter
{
  public string Encrypt(string password)
  {
    var passwordHash = BC.HashPassword(password, 8);

    return passwordHash;
  }

  public bool Verify(string password, string passwordHash)
  {
    return BC.Verify(password, passwordHash);
  }
}
