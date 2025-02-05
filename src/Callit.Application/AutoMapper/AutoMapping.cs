using AutoMapper;
using Callit.Communication.Requests;
using Callit.Communication.Responses;
using Callit.Domain.Entities;

namespace Callit.Application.AutoMapper;

public class AutoMapping : Profile
{
	public AutoMapping()
	{
		RequestToEntity();
		EntityToResponse();
	}

	private void RequestToEntity()
	{
		CreateMap<RequestTicketJson, Ticket>();
	}

	private void EntityToResponse()
	{
		CreateMap<Ticket, ResponseCreatedTicketJson>();
		CreateMap<Ticket, ResponseShortTicketJson>();
		CreateMap<Ticket, ResponseTicketJson>();
		CreateMap<User, ResponseCreatedUserJson>();
	}
}