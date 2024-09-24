using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Addresses.Commands.Create;
using Assembly.Horizon.Application.CQ.Addresses.Commands.Update;
using Assembly.Horizon.Application.CQ.Addresses.Queries.Retrieve;
using Assembly.Horizon.Application.CQ.Addresses.Queries.RetrieveAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Assembly.Horizon.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressController(ISender sender) : Controller
{
    [HttpGet("RetrieveAll")]
    public async Task<IActionResult> RetrieveAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new RetrieveAllAddressQuery(), cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(result.Error);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Retrieve(Guid id, CancellationToken cancellationToken)
    {
        var query = new RetrieveAddressQuery { Id = id };
        var result = await sender.Send(query, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(result.Error);
    }
    [HttpPost("Create")]
    [ProducesResponseType(typeof(CreateAddressCommand), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> CreateAddress([FromBody] CreateAddressCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.IsSuccess);
        }

        return BadRequest(result.Error);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressCommand command)
    {
        var updatedAddress = await sender.Send(command);

        return updatedAddress != null ? Ok(updatedAddress.IsSuccess) : NotFound("Address Not Found.");
    }
}
