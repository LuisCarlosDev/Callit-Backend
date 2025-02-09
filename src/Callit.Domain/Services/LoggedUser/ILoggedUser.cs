using Callit.Domain.Entities;

namespace Callit.Domain.Services.LoggedUser;

public interface ILoggedUser
{
	Task<User> GetUser();
}