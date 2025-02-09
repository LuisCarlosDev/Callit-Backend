using Callit.Domain.Entities;
using Callit.Domain.Repositories.Tickets;
using Microsoft.EntityFrameworkCore;

namespace Callit.Infrastructure.DataAccess.Repositories;

internal class TicketRepository : ITicketRepository
{
	private readonly CallitDbContext _dbContext;
	
	public TicketRepository(CallitDbContext dbContext)
	{
		_dbContext = dbContext;	
	}
	public async Task CreateTicket(Ticket ticket)
	{
		await _dbContext.Tickets.AddAsync(ticket);
	}

	public async Task<List<Ticket>> GetTickets(User user)
	{
		return await _dbContext.Tickets.AsNoTracking().Where(ticket => ticket.UserId == user.Id).ToListAsync();
	}

	public async Task<Ticket?> GetTicketById(Guid id, User user)
	{
		 return await _dbContext.Tickets.AsNoTracking().FirstOrDefaultAsync(ticket => ticket.Id == id && user.Id == ticket.UserId);
	}

	public async Task DeleteTicket(Guid id)
	{
		var ticket = await _dbContext.Tickets.FindAsync(id);

		_dbContext.Tickets.Remove(ticket!);
	}

	public async Task<Ticket?> SearchTicketById(Guid id, User user)
	{
		return await _dbContext.Tickets.FirstOrDefaultAsync(ticket => ticket.Id == id && ticket.UserId == user.Id);
	}

	public void UpdateTicket(Ticket ticket)
	{
		_dbContext.Tickets.Update(ticket);
	}
}