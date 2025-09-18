using AutoMapper;
using Callit.Communication.Responses;
using Callit.Domain.Repositories.Tickets;
using Callit.Domain.Services.LoggedUser;

namespace Callit.Application.UseCases.Tickets.ListTickets;

public class ListTicketsUseCase : IListTicketsUseCase
{
  private readonly ITicketRepository _ticketRepository;
  private readonly IMapper _mapper;
  private readonly ILoggedUser _loggedUser;

  public ListTicketsUseCase(ITicketRepository repository, IMapper mapper, ILoggedUser loggedUser)
  {
    _ticketRepository = repository;
    _mapper = mapper;
    _loggedUser = loggedUser;
  }

  public async Task<ResponseTicketsJson> Execute()
  {
    var loggedUser = await _loggedUser.GetUser();

    var tickets = await _ticketRepository.GetTickets(loggedUser);

    return new ResponseTicketsJson
    {
      Tickets = _mapper.Map<List<ResponseShortTicketJson>>(tickets),
    };
  }
}
