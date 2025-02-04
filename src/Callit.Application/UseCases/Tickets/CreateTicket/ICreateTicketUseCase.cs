using Callit.Communication.Requests;
using Callit.Communication.Responses;

namespace Callit.Application.UseCases.Tickets.CreateTicket;

public interface ICreateTicketUseCase
{
	Task<ResponseCreatedTicketJson> Execute(RequestTicketJson request);
}