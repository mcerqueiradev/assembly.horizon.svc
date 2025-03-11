using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Realtors.Command.Create;
using Assembly.Horizon.Application.CQ.Realtors.Queries.Retrieve;
using Assembly.Horizon.Application.CQ.Realtors.Queries.RetrieveAll;
using Assembly.Horizon.Application.CQ.Realtors.Queries.RetrieveByUserId;
using Assembly.Horizon.Application.CQ.Users.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Assembly.Horizon.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RealtorController(ISender sender) : Controller
{
    [HttpPost("Register")]
    [ProducesResponseType(typeof(CreateUserResponse), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> CreateRealtor([FromBody] CreateRealtorCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.IsSuccess);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("RetrieveAll")]
    public async Task<IActionResult> RetrieveAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new RetrieveAllRealtorsQuery(), cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Error);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Retrieve(Guid id, CancellationToken cancellationToken)
    {
        var query = new RetrieveRealtorQuery { Id = id };
        var result = await sender.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Error);
    }

    [HttpGet("ByUserId/{userId}")]
    public async Task<IActionResult> RetrieveByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var query = new RetrieveRealtorByUserIdQuery { UserId = userId };
        var result = await sender.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Error);
    }
}
