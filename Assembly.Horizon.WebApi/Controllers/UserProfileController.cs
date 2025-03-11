using Assembly.Horizon.Application.CQ.UserProfiles.Commands.Update;
using Assembly.Horizon.Application.CQ.UserProfiles.Queries.Retrieve;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Assembly.Horizon.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserProfileController(ISender sender) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Retrieve(Guid id, CancellationToken cancellationToken)
    {
        var query = new RetrieveUserProfileQuery(id);
        var result = await sender.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Error);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromForm] UpdateUserProfileCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }
}
