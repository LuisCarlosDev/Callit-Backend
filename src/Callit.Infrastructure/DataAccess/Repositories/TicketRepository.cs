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

	public async Task<List<Ticket>> GetTickets()
	{
		return await _dbContext.Tickets.AsNoTracking().ToListAsync();
	}

	public async Task<Ticket?> GetTicketById(Guid id)
	{
		 return await _dbContext.Tickets.AsNoTracking().FirstOrDefaultAsync(ticket => ticket.Id == id);
	}

	public async Task<bool> DeleteTicket(Guid id)
	{
		var ticket = await _dbContext.Tickets.FirstOrDefaultAsync(ticket => ticket.Id == id);

		if (ticket is null)
		{
			return false;
		}
		
		_dbContext.Tickets.Remove(ticket);
		
		return true;
	}

	/// <summary>
	/// It will only be used in case of any change in the entity.
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public async Task<Ticket?> SearchTicketById(Guid id)
	{
		return await _dbContext.Tickets.FirstOrDefaultAsync(ticket => ticket.Id == id);
	}

	public void UpdateTicket(Ticket ticket)
	{
		_dbContext.Tickets.Update(ticket);
	}
}