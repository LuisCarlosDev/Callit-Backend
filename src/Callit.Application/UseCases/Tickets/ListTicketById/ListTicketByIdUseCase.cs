using AutoMapper;
using Callit.Communication.Responses;
using Callit.Domain.Entities;
using Callit.Domain.Repositories.Tickets;
using Callit.Exception.ExceptionBase;

namespace Callit.Application.UseCases.Tickets.ListTicketById;

public class ListTicketByIdUseCase : IListTicketByIdUseCase
{
	private readonly ITicketRepository _ticketRepository;
	private readonly IMapper _mapper;
	
	public ListTicketByIdUseCase(
		ITicketRepository ticketRepository,
		IMapper mapper
		)
	{
		_ticketRepository = ticketRepository;
		_mapper = mapper;
	}
	
	public async Task<ResponseTicketJson> Execute(Guid id)
	{
		var ticket = await _ticketRepository.GetTicketById(id);

		if (ticket is null)
		{
			throw new NotFoundException("Ticket not found");
		}

		return _mapper.Map<ResponseTicketJson>(ticket);
	}
}