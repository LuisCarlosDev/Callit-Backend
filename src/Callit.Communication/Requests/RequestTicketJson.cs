using Callit.Communication.Enums;

namespace Callit.Communication.Requests;

public class RequestTicketJson
{
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public TicketStatus Status { get; set; } = TicketStatus.Waiting;
	public Priority Priority { get; set; }
	public DateTime Date { get; set;}
	public string System { get; set; } = string.Empty; 
}