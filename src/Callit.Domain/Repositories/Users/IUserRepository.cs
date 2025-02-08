using Callit.Domain.Entities;

namespace Callit.Domain.Repositories.Users;

public interface IUserRepository
{
	Task CreateUser(User user);
	Task<bool> UserAlreadyExists(string email);
}