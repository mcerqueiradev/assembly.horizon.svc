using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Assembly.Horizon.Application.CQ.PropertyNegotiation.Queries.RetrieveByProposal;

public class RetrieveByProposalQuery : IRequest<Result<List<RetrieveByProposalResponse>, Success, Error>>
{
    public Guid ProposalId { get; set; }
}

public class RetrieveByProposalQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<RetrieveByProposalQuery, Result<List<RetrieveByProposalResponse>, Success, Error>>
{
    public async Task<Result<List<RetrieveByProposalResponse>, Success, Error>> Handle(
    RetrieveByProposalQuery request,
    CancellationToken cancellationToken)
    {
        var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";

        var negotiations = await unitOfWork.ProposalNegotiationRepository
            .ListByProposalWithSenderAsync(request.ProposalId);

        var response = negotiations.Select(n => new RetrieveByProposalResponse
        {
            Id = n.Id,
            SenderId = n.SenderId,
            Sender = new SenderResponse
            {
                FirstName = n.Sender.Name.FirstName,
                LastName = n.Sender.Name.LastName,
                ImageUrl = n.Sender.ImageUrl != null ? $"{baseUrl}/uploads/{n.Sender.ImageUrl}" : null,
                Access = n.Sender.Access.ToString(),
            },
            Message = n.Message,
            CounterOffer = n.CounterOffer,
            Status = n.Status.ToString(),
            CreatedAt = n.CreatedAt,
            Documents = n.Documents.Select(d => new DocumentResponse
            {
                Id = d.Id,
                Name = d.DocumentName,
                Path = $"{baseUrl}/uploads/{d.DocumentName}",
                Type = d.Type.ToString(),
                Status = d.Status.ToString()
            }).ToList()
        }).ToList();

        return response;
    }

}

public class RetrieveByProposalResponse
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public SenderResponse Sender { get; set; }
    public string Message { get; set; }
    public decimal? CounterOffer { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<DocumentResponse> Documents { get; set; } = new();
}

public class SenderResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ImageUrl { get; set; }
    public string Access { get; set; }
}

public class DocumentResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
}
