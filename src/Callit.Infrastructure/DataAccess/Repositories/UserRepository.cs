using Callit.Domain.Entities;
using Callit.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace Callit.Infrastructure.DataAccess.Repositories;

internal class UserRepository : IUserRepository
{
	private readonly CallitDbContext _dbContext;
	
	public UserRepository(CallitDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task CreateUser(User user)
	{
		await _dbContext.Users.AddAsync(user);
	}

	public async Task<bool> UserAlreadyExists(string email)
	{
		return await _dbContext.Users.AnyAsync(user => user.Email.Equals(email));
	}
}