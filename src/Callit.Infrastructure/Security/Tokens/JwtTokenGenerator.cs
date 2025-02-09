using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Callit.Domain.Entities;
using Callit.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;

namespace Callit.Infrastructure.Security.Tokens;

internal class JwtTokenGenerator : IAccessTokenGenerator
{
	private readonly uint _expiresIn;
	private readonly string _secret;
	
	public JwtTokenGenerator(uint expiresIn, string secret)
	{
		_expiresIn = expiresIn;
		_secret = secret;
	}
	
	public string GenerateAccessToken(User user)
	{
		var claims = new List<Claim>()
		{
			new Claim(ClaimTypes.Name, user.Name),
			new Claim(ClaimTypes.Sid, user.UserIdentifier.ToString()),
		};
		
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Expires = DateTime.UtcNow.AddMinutes(_expiresIn),
			SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature),
			Subject = new ClaimsIdentity(claims)
		};

		var tokenHandler = new JwtSecurityTokenHandler();
		
		var securityToken = tokenHandler.CreateToken(tokenDescriptor);
		
		return tokenHandler.WriteToken(securityToken);
	}

	private SymmetricSecurityKey SecurityKey()
	{
		var key = Encoding.UTF8.GetBytes(_secret);
		
		return new SymmetricSecurityKey(key);
	}
}