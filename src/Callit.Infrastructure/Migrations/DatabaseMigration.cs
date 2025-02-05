using Callit.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Callit.Infrastructure.Migrations;

public static class DatabaseMigration
{
	public async static Task MigrateDatabase(IServiceProvider serviceProvider)
	{
		var dbContext = serviceProvider.GetRequiredService<CallitDbContext>();

		await dbContext.Database.MigrateAsync();
	}
}