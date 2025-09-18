using AutoMapper;
using Callit.Communication.Responses;
using Callit.Domain.Entities;
using Callit.Domain.Repositories.Tickets;
using Callit.Domain.Services.LoggedUser;
using Callit.Exception.ExceptionBase;

namespace Callit.Application.UseCases.Tickets.ListTicketById;

public class ListTicketByIdUseCase : IListTicketByIdUseCase
{
  private readonly ITicketRepository _ticketRepository;
  private readonly IMapper _mapper;
  private readonly ILoggedUser _loggedUser;

  public ListTicketByIdUseCase(
    ITicketRepository ticketRepository,
    IMapper mapper,
    ILoggedUser loggedUser
  )
  {
    _ticketRepository = ticketRepository;
    _mapper = mapper;
    _loggedUser = loggedUser;
  }

  public async Task<ResponseTicketJson> Execute(Guid id)
  {
    var loggedUser = await _loggedUser.GetUser();

    var ticket = await _ticketRepository.GetTicketById(id, loggedUser);

    if (ticket is null || ticket.UserId != loggedUser.Id)
    {
      throw new NotFoundException("Ticket not found");
    }

    return _mapper.Map<ResponseTicketJson>(ticket);
  }
}
