using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Transactions.Commands.Create;
using Assembly.Horizon.Application.CQ.Transactions.Commands.Payment.Completed;
using Assembly.Horizon.Application.CQ.Transactions.Queries.RetrieveByUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Assembly.Horizon.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController(ISender sender) : Controller
{
    [HttpPost("Register")]
    [ProducesResponseType(typeof(CreateTransactionResponse), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> CreateRealtor([FromBody] CreateTransactionCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.IsSuccess);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("user/{userId}")]
    [ProducesResponseType(typeof(RetrieveTransactionByUserResponse), 200)]
    [ProducesResponseType(typeof(Error), 404)]
    public async Task<IActionResult> GetTransactionsByUser(Guid userId, CancellationToken cancellationToken)
    {
        var query = new RetrieveTransactionByUserQuery(userId);
        var result = await sender.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    [HttpPut("{id}/process")]
    [ProducesResponseType(typeof(CompletedTransactionResponse), 200)]
    [ProducesResponseType(typeof(Error), 404)]
    public async Task<IActionResult> ProcessPayment(Guid id, [FromBody] CompletedTransactionCommand command, CancellationToken cancellationToken)
    {
        command = command with { TransactionId = id };
        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

}
