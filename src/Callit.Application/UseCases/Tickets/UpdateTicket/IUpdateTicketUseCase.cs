using Callit.Communication.Requests;
using Callit.Communication.Responses;

namespace Callit.Application.UseCases.Tickets.UpdateTicket;

public interface IUpdateTicketUseCase
{
  Task Execute(Guid id, RequestTicketJson ticket);
}
