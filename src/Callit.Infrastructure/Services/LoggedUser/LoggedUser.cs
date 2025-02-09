using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Callit.Domain.Entities;
using Callit.Domain.Security.Tokens;
using Callit.Domain.Services.LoggedUser;
using Callit.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Callit.Infrastructure.Services.LoggedUser;

public class LoggedUser : ILoggedUser
{
	private readonly CallitDbContext _dbContext;
	private readonly ITokenProvider _tokenProvider;
	
	public LoggedUser(CallitDbContext dbContext, ITokenProvider tokenProvider) {
		_dbContext = dbContext;
		_tokenProvider = tokenProvider;
	}
	
	public async Task<User> GetUser()
	{
		string token = _tokenProvider.TokenOnRequest();

		var tokenHandler = new JwtSecurityTokenHandler();

		var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

		var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

		return await _dbContext.Users.AsNoTracking().FirstAsync(user => user.UserIdentifier == Guid.Parse(identifier));
	}
}