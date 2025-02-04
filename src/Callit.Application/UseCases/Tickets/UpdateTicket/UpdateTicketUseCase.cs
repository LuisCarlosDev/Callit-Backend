using AutoMapper;
using Callit.Application.UseCases.Tickets.CreateTicket;
using Callit.Communication.Requests;
using Callit.Communication.Responses;
using Callit.Domain.Repositories;
using Callit.Domain.Repositories.Tickets;
using Callit.Exception.ExceptionBase;

namespace Callit.Application.UseCases.Tickets.UpdateTicket;

public class UpdateTicketUseCase : IUpdateTicketUseCase
{
	private readonly IMapper _mapper;
	private readonly IUnitOfWork _unitOfWork;
	private readonly ITicketRepository _ticketRepository;
	
	public UpdateTicketUseCase(IMapper mapper, IUnitOfWork unitOfWork, ITicketRepository ticketRepository)
	{
		_mapper = mapper;
		_unitOfWork = unitOfWork;
		_ticketRepository = ticketRepository;
	}
	public async Task Execute(Guid id, RequestTicketJson request)
	{
		Validate(request);

		var ticket = await _ticketRepository.SearchTicketById(id);

		if (ticket is null)
		{
			throw new NotFoundException("Ticket not found");
		}
		
		_mapper.Map(request, ticket);
		
		_ticketRepository.UpdateTicket(ticket);

		await _unitOfWork.Commit();
	}

	private void Validate(RequestTicketJson ticket)
	{
		var validator = new TicketValidator();

		var result = validator.Validate(ticket);

		if (result.IsValid == false)
		{
			var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

			throw new ErrorOnValidationException(errorMessages);
		}
	}
}