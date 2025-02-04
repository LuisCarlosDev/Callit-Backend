namespace Callit.Application.UseCases.Tickets.DeleteTicket;

public interface IDeleteTicketUseCase
{
	Task Execute(Guid id);
}