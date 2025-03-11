using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Properties.Commands.Create;
using Assembly.Horizon.Application.CQ.Properties.Commands.TogglePropertyActive;
using Assembly.Horizon.Application.CQ.Properties.Queries.Retrieve;
using Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveAll;
using Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveByRealtor;
using Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveLatest;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Assembly.Horizon.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PropertyController(ISender sender) : Controller
{
    [HttpPost("Create")]
    [ProducesResponseType(typeof(CreatePropertyResponse), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> CreateProperty([FromForm] CreatePropertyCommand command, CancellationToken cancellationToken)
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
        var result = await sender.Send(new RetrieveAllPropertiesQuery(), cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Error);
    }

    [HttpGet("RetrieveAllByRealtor/{realtorId}")]
    public async Task<IActionResult> RetrieveAllByRealtor(Guid realtorId, CancellationToken cancellationToken)
    {
        var query = new RetrieveAllPropertiesByRealtorQuery { RealtorId = realtorId };
        var result = await sender.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Error);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Retrieve(Guid id, CancellationToken cancellationToken)
    {
        var query = new RetrievePropertyQuery { Id = id };
        var result = await sender.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Error);
    }

    [HttpPatch("{id}/toggle-active")]
    [ProducesResponseType(typeof(TogglePropertyActiveResponse), 200)]
    [ProducesResponseType(typeof(Error), 404)]
    public async Task<IActionResult> ToggleActive(Guid id, CancellationToken cancellationToken)
    {
        var command = new TogglePropertyActiveCommand(id);
        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Error);
    }



    [HttpGet("RetrieveLatest")]
    public async Task<IActionResult> RetrieveLatest(CancellationToken cancellationToken)
    {
        var query = new RetrieveLatestPropertyQuery();
        var result = await sender.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Error);
    }
}
