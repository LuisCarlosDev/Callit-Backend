using AutoMapper;
using Callit.Communication.Responses;

namespace Callit.Application.UseCases.Tickets.ListTickets;

public interface IListTicketsUseCase
{
  Task<ResponseTicketsJson> Execute();
}
