using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.PropertyNegotiation.Commands;
using Assembly.Horizon.Application.CQ.PropertyNegotiation.Commands.AcceptNegotiation;
using Assembly.Horizon.Application.CQ.PropertyNegotiation.Queries.RetrieveByProposal;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Assembly.Horizon.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PropertyNegotiationController(ISender sender) : ControllerBase
{
    [HttpPost("Create")]
    [ProducesResponseType(typeof(CreateProposalNegotiationCommand), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> CreatePropertyVisit([FromForm] CreateProposalNegotiationCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.IsSuccess);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("ByProposal/{proposalId}")]
    [ProducesResponseType(typeof(List<RetrieveByProposalResponse>), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> GetByProposal(Guid proposalId, CancellationToken cancellationToken)
    {
        var query = new RetrieveByProposalQuery { ProposalId = proposalId };
        var result = await sender.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpPatch("/api/Proposal/{proposalId}/negotiations/{negotiationId}/accept")]
    [ProducesResponseType(typeof(AcceptNegotiationResponse), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> AcceptNegotiation(Guid proposalId, Guid negotiationId, CancellationToken cancellationToken)
    {
        var command = new AcceptNegotiationCommand
        {
            ProposalId = proposalId,
            NegotiationId = negotiationId
        };

        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    //[HttpPatch("{proposalId}/negotiations/{negotiationId}/reject")]
    //[ProducesResponseType(typeof(RejectNegotiationResponse), 200)]
    //[ProducesResponseType(typeof(Error), 400)]
    //public async Task<IActionResult> RejectNegotiation(Guid proposalId, Guid negotiationId, CancellationToken cancellationToken)
    //{
    //    var command = new RejectNegotiationCommand
    //    {
    //        ProposalId = proposalId,
    //        NegotiationId = negotiationId
    //    };

    //    var result = await sender.Send(command, cancellationToken);

    //    if (result.IsSuccess)
    //    {
    //        return Ok(result.Value);
    //    }

    //    return BadRequest(result.Error);
    //}
}
