using Assembly.Horizon.Application.CQ.Users.Queries.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Assembly.Horizon.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
   private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthUserQuery query)
    {
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            return Unauthorized(result.Error);
        }

        return Ok(new
        {
            Token = result.Value.Token,
            UserId = result.Value.UserId
        });
    }

}
