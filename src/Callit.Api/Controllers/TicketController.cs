using Callit.Application.UseCases.Tickets.CreateTicket;
using Callit.Application.UseCases.Tickets.DeleteTicket;
using Callit.Application.UseCases.Tickets.ListTicketById;
using Callit.Application.UseCases.Tickets.ListTickets;
using Callit.Application.UseCases.Tickets.UpdateTicket;
using Callit.Communication.Requests;
using Callit.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Callit.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketController: ControllerBase {
	[HttpPost]
	[ProducesResponseType(typeof(ResponseCreatedTicketJson), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> CreateUser(
		[FromServices] ICreateTicketUseCase useCase,
		[FromBody] RequestTicketJson request
		)
	{
		var response = await useCase.Execute(request);

		return Created(string.Empty, response);
	}
	
	[HttpGet]
	[ProducesResponseType(typeof(ResponseTicketsJson), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	public async Task<IActionResult> ListTickets([FromServices] IListTicketsUseCase useCase)
	{
		var tickets = await useCase.Execute();

		if (tickets.Tickets.Count != 0)
			return Ok(tickets);
		
		return NoContent();
	}

	[HttpGet]
	[Route("{id:guid}")]
	[ProducesResponseType(typeof(ResponseTicketJson), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ResponseTicketJson),StatusCodes.Status404NotFound)]
	public async Task<IActionResult> ListTicketById(
		[FromServices] IListTicketByIdUseCase useCase,
		[FromRoute] Guid id
		)
	{
		var ticket = await useCase.Execute(id);
		
		return Ok(ticket);
	}

	[HttpDelete]
	[Route("{id:guid}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
	public async Task<IActionResult> DeleteTicket(
		[FromServices] IDeleteTicketUseCase useCase,
		[FromRoute] Guid id)
	{
		await useCase.Execute(id);
		
		return NoContent();
	}

	[HttpPut]
	[Route("{id:guid}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
	public async Task<IActionResult> UpdateTicket(
		[FromServices] IUpdateTicketUseCase useCase,
		[FromRoute] Guid id,
		[FromBody] RequestTicketJson ticket
		)
	{
		await useCase.Execute(id, ticket);
		
		return NoContent();
	}
}