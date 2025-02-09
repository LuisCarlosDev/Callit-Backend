using Callit.Application.UseCases.SingIn.SignInUser;
using Callit.Communication.Requests;
using Callit.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Callit.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SigninController : ControllerBase
{
	[HttpPost]
	[ProducesResponseType(typeof(ResponseCreatedUserJson), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> SignIn(
		[FromServices] ISignInUserUseCase useCase,
		[FromBody] RequestSignInJson request
		)
	{
		var user = await useCase.Execute(request);
		
		return Ok(user);
	}
}