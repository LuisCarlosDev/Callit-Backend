using Callit.Domain.Entities;

namespace Callit.Domain.Repositories.Tickets;

public interface ITicketRepository
{
  Task CreateTicket(Ticket ticket);
  Task<List<Ticket>> GetTickets(User user);
  Task<Ticket?> GetTicketById(Guid id, User user);

  /// <summary>
  /// This function returns TRUE if the deletion was successful
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  Task DeleteTicket(Guid id);

  Task<Ticket?> SearchTicketById(Guid id, User user);
  void UpdateTicket(Ticket ticket);
}
