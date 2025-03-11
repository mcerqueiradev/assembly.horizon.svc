using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.PropertyNegotiation.Commands.Create;
using Assembly.Horizon.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Assembly.Horizon.Application.CQ.PropertyNegotiation.Commands;

public class CreateProposalNegotiationCommand : IRequest<Result<CreateProposalNegotiationResponse, Success, Error>>
{
    public Guid ProposalId { get; set; }
    public Guid SenderId { get; set; }
    public string Message { get; set; }
    public decimal? CounterOffer { get; set; }
    public IFormFile[]? Documents { get; set; }
    public List<DocumentType> DocumentTypes { get; set; } = new();
}


public class DocumentInfo
{
    public string Name { get; set; }
    public string Path { get; set; }
    public DocumentType Type { get; set; }
}
