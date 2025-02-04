using Callit.Domain.Entities;

namespace Callit.Domain.Repositories.Tickets;

public interface ITicketRepository
{
	Task CreateTicket(Ticket ticket);
	Task<List<Ticket>> GetTickets();
	Task<Ticket?> GetTicketById(Guid id);
	/// <summary>
	/// This function returns TRUE if the deletion was successful
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	Task<bool> DeleteTicket(Guid id);
	
	Task<Ticket?> SearchTicketById(Guid id);
	void UpdateTicket(Ticket ticket);
}