using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Comments.Commands;
using Assembly.Horizon.Application.CQ.Comments.Commands.Create;
using Assembly.Horizon.Application.CQ.Comments.Queries.RetrieveByProperty;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Assembly.Horizon.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController(ISender sender) : Controller
{
    [HttpPost("Create")]
    [ProducesResponseType(typeof(CreateCommentResponse), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> CreateComment([FromForm] CreateCommentCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.IsSuccess);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("property/{propertyId}")]
    [ProducesResponseType(typeof(IEnumerable<CreateCommentResponse>), 200)]
    [ProducesResponseType(typeof(Error), 404)]
    public async Task<IActionResult> GetCommentsByPropertyId(Guid propertyId, CancellationToken cancellationToken)
    {
        var query = new RetrieveCommentsByPropertyQuery { PropertyId = propertyId };
        var result = await sender.Send(query, cancellationToken);

        if (result.IsSuccess && result.Value != null)
        {
            return Ok(result.Value); // Certifique-se de que result.Value é uma lista de comentários
        }

        return NotFound("Comments not found for the specified property.");
    }

    [HttpPost("ToggleHelpful")]
    [ProducesResponseType(typeof(ToggleCommentHelpfulResponse), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> ToggleCommentHelpful([FromBody] ToggleCommentHelpfulCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

}
