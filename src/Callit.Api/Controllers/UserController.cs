using Callit.Application.UseCases.Users.CreateUser;
using Callit.Communication.Requests;
using Callit.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Callit.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	[HttpPost]
	[ProducesResponseType(typeof(ResponseCreatedUserJson), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> CreateUser(
		[FromServices] ICreateUserUseCase useCase,
		[FromBody] RequestUserJson request
	)
	{
		var response = await useCase.Execute(request);

		return Created(string.Empty, response);
	}
}