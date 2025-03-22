using AutoMapper;
using Callit.Application.UseCases.Tickets.CreateTicket;
using Callit.Communication.Requests;
using Callit.Communication.Responses;
using Callit.Domain.Repositories;
using Callit.Domain.Repositories.Tickets;
using Callit.Domain.Services.LoggedUser;
using Callit.Exception.ExceptionBase;

namespace Callit.Application.UseCases.Tickets.UpdateTicket;

public class UpdateTicketUseCase : IUpdateTicketUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITicketRepository _ticketRepository;
    private readonly ILoggedUser _loggedUser;

    public UpdateTicketUseCase(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ITicketRepository ticketRepository,
        ILoggedUser loggedUser
    )
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _ticketRepository = ticketRepository;
        _loggedUser = loggedUser;
    }

    public async Task Execute(Guid id, RequestTicketJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.GetUser();

        var ticket = await _ticketRepository.SearchTicketById(id, loggedUser);

        if (ticket is null || ticket.UserId != loggedUser.Id)
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
