using Callit.Domain.Repositories;
using Callit.Domain.Repositories.Tickets;
using Callit.Domain.Security.BCrypt;
using Callit.Infrastructure.DataAccess;
using Callit.Infrastructure.DataAccess.Repositories;
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

		services.AddScoped<IPasswordEncripter, Security.BCrypt>();
	}

	private static void AddRepositories(IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<ITicketRepository, TicketRepository>();
	}
	
	private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("ApiDatabase");
		services.AddDbContext<CallitDbContext>(config => config.UseNpgsql(connectionString));
	}
}