using Assembly.Horizon.Application.CQ.UserPosts.Commands.Create;
using Assembly.Horizon.Application.CQ.UserPosts.Queries.RetrieveAllUserPost;
using Assembly.Horizon.Application.CQ.UserPosts.Queries.RetrieveUserPost;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Assembly.Horizon.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserPostController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateUserPostCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Retrieve(Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new RetrieveUserPostQuery { Id = id }, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Error);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> RetrieveAll(Guid userId, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new RetrieveAllUserPostQuery { UserId = userId }, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Error);
    }
}
