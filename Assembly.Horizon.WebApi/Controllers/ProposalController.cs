using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.PropertyProposal.Commands.Accept;
using Assembly.Horizon.Application.CQ.PropertyProposal.Commands.Create;
using Assembly.Horizon.Application.CQ.PropertyProposal.Queries.Retrieve;
using Assembly.Horizon.Application.CQ.PropertyProposal.Queries.RetrieveByRealtor;
using Assembly.Horizon.Application.CQ.PropertyProposal.Queries.RetrieveByUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Assembly.Horizon.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProposalController(ISender sender) : ControllerBase
{

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] CreatePropertyProposalCommand command)
    {
        var result = await sender.Send(command);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("ByRealtor/{realtorId}")]
    public async Task<IActionResult> GetByRealtor(Guid realtorId)
    {
        var query = new RetrieveByRealtorQuery { RealtorId = realtorId };
        var result = await sender.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new RetrieveProposalQuery(id);
        var result = await sender.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("ByUser/{userId}")]
    public async Task<IActionResult> GetByUser(Guid userId)
    {
        var query = new RetrieveByUserQuery { UserId = userId };
        var result = await sender.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpPatch("{proposalId}/accept")]
    [ProducesResponseType(typeof(AcceptProposalResponse), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> AcceptProposal(Guid proposalId, CancellationToken cancellationToken)
    {
        var command = new AcceptProposalCommand
        {
            ProposalId = proposalId
        };

        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

}
