using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.PropertyVisits.Commands.ConfirmVisitCommand;
using Assembly.Horizon.Application.CQ.PropertyVisits.Commands.Create;
using Assembly.Horizon.Application.CQ.PropertyVisits.Commands.DeclineVisitCommand;
using Assembly.Horizon.Application.CQ.PropertyVisits.Queries.Retrieve;
using Assembly.Horizon.Application.CQ.PropertyVisits.Queries.RetrieveAll;
using Assembly.Horizon.Application.CQ.PropertyVisits.Queries.RetrieveAllByUser;
using Assembly.Horizon.Application.CQ.PropertyVisits.Queries.RetrieveAvailableTimeSlotsQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Assembly.Horizon.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyVisitController(ISender sender) : Controller
{
    [HttpPost("Create")]
    [ProducesResponseType(typeof(CreatePropertyVisitCommand), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> CreatePropertyVisit([FromBody] CreatePropertyVisitCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.IsSuccess);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Retrieve(Guid id, CancellationToken cancellationToken)
    {
        var query = new RetrievePropertyVisitQuery { UserId = id };
        var result = await sender.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Error);
    }

    [HttpGet("RetrieveAll")]
    public async Task<IActionResult> RetrieveAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new RetrieveAllPropertyVisitsQuery(), cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Error);
    }

    [HttpGet("available-slots")]
    [ProducesResponseType(typeof(GetAvailableTimeSlotsResponse), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> GetAvailableTimeSlots([FromQuery] Guid propertyId, [FromQuery] string date, CancellationToken cancellationToken)
    {
        var query = new GetAvailableTimeSlotsQuery { PropertyId = propertyId, Date = date };
        var result = await sender.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> RetrieveAllByUser(Guid userId, CancellationToken cancellationToken)
    {
        var query = new RetrieveAllPropertyVisitsByUserQuery { UserId = userId };
        var result = await sender.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return NotFound(result.Error);
    }

    [HttpGet("confirm/{token}")]
    [ProducesResponseType(typeof(ConfirmVisitResponse), 200)]
    [ProducesResponseType(typeof(Error), 404)]
    public async Task<IActionResult> ConfirmVisit(string token, CancellationToken cancellationToken)
    {
        var command = new ConfirmVisitCommand(token);
        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Error);
    }

    [HttpGet("decline/{token}")]
    [ProducesResponseType(typeof(DeclineVisitResponse), 200)]
    [ProducesResponseType(typeof(Error), 404)]
    public async Task<IActionResult> DeclineVisit(string token, CancellationToken cancellationToken)
    {
        var command = new DeclineVisitCommand(token);
        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Error);
    }
}
