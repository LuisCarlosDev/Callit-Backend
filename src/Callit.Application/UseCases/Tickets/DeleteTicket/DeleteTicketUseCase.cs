using AutoMapper;
using Callit.Domain.Repositories;
using Callit.Domain.Repositories.Tickets;
using Callit.Domain.Services.LoggedUser;
using Callit.Exception.ExceptionBase;

namespace Callit.Application.UseCases.Tickets.DeleteTicket;

public class DeleteTicketUseCase : IDeleteTicketUseCase
{
	private readonly ITicketRepository _ticketRepository;
	private readonly IUnitOfWork _unitOfWork;
	private readonly ILoggedUser _loggedUser;
	
	public DeleteTicketUseCase(
		ITicketRepository repository, 
		IUnitOfWork unitOfWork,
		ILoggedUser loggedUser)
	{
		_ticketRepository = repository;
		_unitOfWork = unitOfWork;
		_loggedUser = loggedUser;
	}
	
	public async Task Execute(Guid id)
	{
		var loggedUser = await _loggedUser.GetUser();

		var ticket = await _ticketRepository.SearchTicketById(id, loggedUser);
		
		if (ticket is null)
		{
			throw new NotFoundException("Ticket not found");
		}
		
		await _ticketRepository.DeleteTicket(id);
		
		await _unitOfWork.Commit();
	}
}