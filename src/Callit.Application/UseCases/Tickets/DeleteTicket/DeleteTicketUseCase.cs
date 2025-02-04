using AutoMapper;
using Callit.Domain.Repositories;
using Callit.Domain.Repositories.Tickets;
using Callit.Exception.ExceptionBase;

namespace Callit.Application.UseCases.Tickets.DeleteTicket;

public class DeleteTicketUseCase : IDeleteTicketUseCase
{
	private readonly ITicketRepository _ticketRepository;
	private readonly IUnitOfWork _unitOfWork;
	
	public DeleteTicketUseCase(ITicketRepository repository, IUnitOfWork unitOfWork)
	{
		_ticketRepository = repository;
		_unitOfWork = unitOfWork;
	}
	
	public async Task Execute(Guid id)
	{
		var ticket = await _ticketRepository.DeleteTicket(id);

		if (ticket == false)
		{
			throw new NotFoundException("Ticket not found");
		}
		
		await _unitOfWork.Commit();
	}
}