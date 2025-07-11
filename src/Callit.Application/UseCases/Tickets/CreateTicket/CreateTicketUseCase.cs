using AutoMapper;
using Callit.Communication.Requests;
using Callit.Communication.Responses;
using Callit.Domain.Entities;
using Callit.Domain.Repositories;
using Callit.Domain.Repositories.Tickets;
using Callit.Domain.Services.LoggedUser;
using Callit.Exception.ExceptionBase;

namespace Callit.Application.UseCases.Tickets.CreateTicket;

public class CreateTicketUseCase : ICreateTicketUseCase
{
  private readonly ITicketRepository _repository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;
  private readonly ILoggedUser _loggedUser;

  public CreateTicketUseCase(
    ITicketRepository repository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILoggedUser loggedUser
  )
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
    _loggedUser = loggedUser;
  }

  public async Task<ResponseCreatedTicketJson> Execute(RequestTicketJson request)
  {
    Validate(request);

    var loggedUser = await _loggedUser.GetUser();

    var ticket = _mapper.Map<Ticket>(request);
    ticket.UserId = loggedUser.Id;

    await _repository.CreateTicket(ticket);
    await _unitOfWork.Commit();

    return _mapper.Map<ResponseCreatedTicketJson>(ticket);
  }

  private void Validate(RequestTicketJson request)
  {
    var validator = new TicketValidator();

    var result = validator.Validate(request);

    if (result.IsValid == false)
    {
      var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

      throw new ErrorOnValidationException(errorMessages);
    }
  }
}
