using AutoMapper;
using Callit.Communication.Responses;
using Callit.Domain.Repositories.Tickets;

namespace Callit.Application.UseCases.Tickets.ListTickets;

public class ListTicketsUseCase : IListTicketsUseCase
{
	private readonly ITicketRepository _ticketRepository;
	private readonly IMapper _mapper;
	
	public ListTicketsUseCase(ITicketRepository repository, IMapper mapper)
	{
		_ticketRepository = repository;
		_mapper = mapper;
	}
	
	public async Task<ResponseTicketsJson> Execute()
	{
		var tickets = await _ticketRepository.GetTickets();

		return new ResponseTicketsJson
		{
			Tickets = _mapper.Map<List<ResponseShortTicketJson>>(tickets)
		};
	}
}