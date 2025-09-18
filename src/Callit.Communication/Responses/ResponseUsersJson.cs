using Callit.Communication.Enums;

namespace Callit.Communication.Responses;

public class ResponseUsersJson
{
  public string Name { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string Role { get; set; } = Roles.User;
}
