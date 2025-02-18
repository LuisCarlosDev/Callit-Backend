using Callit.Application.UseCases.Tickets;
using Callit.Communication.Enums;
using Callit.Communication.Requests;

namespace Validators.Tests.Tickets.CreateTicket;

public class CreateTicketValidatorTest
{
    [Fact]
    public void CreateTicketSucessfully()
    {
        var validator = new TicketValidator();

        var request = new RequestTicketJson
        {
            Title = "Title",
            Description = "Description",
            Date = DateTime.Now,
            Priority = Priority.Low,
            Status = TicketStatus.Waiting,
            System = "System",
        };

        var ticket = validator.Validate(request);

        Assert.True(ticket.IsValid);
    }
}
