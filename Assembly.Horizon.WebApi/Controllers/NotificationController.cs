using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Notifications.Commands.Create;
using Assembly.Horizon.Application.CQ.Notifications.Commands.MarkNotificationAsReadCommand;
using Assembly.Horizon.Application.CQ.Notifications.Queries.GetRecentNotificationsQuery;
using Assembly.Horizon.Application.CQ.Notifications.Queries.RetrieveNotificationsQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Assembly.Horizon.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController(ISender sender) : Controller
{
    [HttpPost]
    [ProducesResponseType(typeof(Result<CreateNotificationResponse, Success, Error>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateNotificationCommand command)
    {
        var result = await sender.Send(command);

        if (result.IsSuccess)
        {
            return Created($"api/notifications/{result.Value.NotificationId}", result);
        }

        return BadRequest(result.Error);
    }

    [HttpGet]
    public async Task<IActionResult> GetNotifications([FromQuery] Guid userId)
    {
        var result = await sender.Send(new GetNotificationsQuery(userId));

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Error);
    }

    [HttpGet("recent")]
    public async Task<IActionResult> GetRecentNotifications([FromQuery] Guid userId)
    {
        var result = await sender.Send(new GetRecentNotificationsQuery(userId));

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Error);
    }

    [HttpPatch("{id}/read")]
    public async Task<IActionResult> MarkAsRead(Guid id)
    {
        var result = await sender.Send(new MarkNotificationAsReadCommand(id));

        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return NotFound(result.Error);
    }

}
