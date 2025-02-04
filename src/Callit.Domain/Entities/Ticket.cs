using Callit.Domain.Enums;

namespace Callit.Domain.Entities;

public class Ticket
{
	public Guid Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public TicketStatus Status { get; set; } = TicketStatus.Waiting;
	public Priority Priority { get; set; }
	public DateTime Date { get; set;}
}