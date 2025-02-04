using Callit.Communication.Requests;
using FluentValidation;

namespace Callit.Application.UseCases.Tickets;

public class TicketValidator : AbstractValidator<RequestTicketJson>
{
	public TicketValidator()
	{
		RuleFor(ticket => ticket.Title).NotEmpty().WithMessage("Title is required");
		RuleFor(ticket => ticket.Description).NotEmpty().WithMessage("Description is required");
	}
}