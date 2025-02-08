using Callit.Domain.Enums;

namespace Callit.Domain.Entities;

public class User
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public Guid UserIdentifier  { get; set; }
	public string Role { get; set; } = Roles.User;
}