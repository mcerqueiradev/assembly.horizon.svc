using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Contracts.Commands.Create;
using Assembly.Horizon.Application.CQ.Properties.Commands.Create;
using Assembly.Horizon.Application.CQ.Users.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Assembly.Horizon.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContractController(ISender sender) : Controller
{
    [HttpPost("Create")]
    [ProducesResponseType(typeof(CreateContractResponse), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> CreateContract([FromForm] CreateContractCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.IsSuccess);
        }

        return BadRequest(result.Error);
    }
}
