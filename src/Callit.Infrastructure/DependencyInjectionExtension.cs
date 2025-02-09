using Callit.Domain.Repositories;
using Callit.Domain.Repositories.Tickets;
using Callit.Domain.Repositories.Users;
using Callit.Domain.Security.BCrypt;
using Callit.Domain.Security.Tokens;
using Callit.Domain.Services.LoggedUser;
using Callit.Infrastructure.DataAccess;
using Callit.Infrastructure.DataAccess.Repositories;
using Callit.Infrastructure.Security.Tokens;
using Callit.Infrastructure.Services.LoggedUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Callit.Infrastructure;

public static class DependencyInjectionExtension
{
	public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		AddRepositories(services);
		AddDbContext(services, configuration);
		AddToken(services, configuration);

		services.AddScoped<IPasswordEncripter, Security.BCrypt>();
		services.AddScoped<ILoggedUser, LoggedUser>();
	}

	private static void AddToken(IServiceCollection services, IConfiguration configuration)
	{
		var expirationInMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresIn");
		var secretKey = configuration.GetValue<string>("Settings:Jwt:Secret");

		services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationInMinutes, secretKey!));
	}

	private static void AddRepositories(IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<ITicketRepository, TicketRepository>();
		services.AddScoped<IUserRepository, UserRepository>();
	}
	
	private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("ApiDatabase");
		services.AddDbContext<CallitDbContext>(config => config.UseNpgsql(connectionString));
	}
}