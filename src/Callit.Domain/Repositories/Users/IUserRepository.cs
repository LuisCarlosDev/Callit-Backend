using Callit.Domain.Entities;

namespace Callit.Domain.Repositories.Users;

public interface IUserRepository
{
  Task<List<User>> ListUsers();
  Task CreateUser(User user);
  Task<bool> UserAlreadyExists(string email);
  Task<User?> GetUserByEmail(string email);
}
