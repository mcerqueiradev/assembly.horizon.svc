using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Transactions.Commands.Create;
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
}
