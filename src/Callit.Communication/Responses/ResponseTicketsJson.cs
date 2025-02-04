using Callit.Communication.Enums;

namespace Callit.Communication.Responses;

public class ResponseTicketsJson
{
	public List<ResponseShortTicketJson> Tickets { get; set; } = [];
}
