using Callit.Communication.Responses;
using Callit.Domain.Entities;

namespace Callit.Application.UseCases.Tickets.ListTicketById;

public interface IListTicketByIdUseCase
{
	Task<ResponseTicketJson> Execute(Guid id);
}