using Callit.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Callit.Infrastructure.DataAccess;

public class CallitDbContext : DbContext
{
	
	public CallitDbContext(DbContextOptions<CallitDbContext> options): base(options) { }
  	
	public DbSet<Ticket> Tickets { get; set; }
	public DbSet<User> Users { get; set; }
}