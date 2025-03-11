using Assembly.Horizon.Domain.Model;

namespace Assembly.Horizon.Application.CQ.PropertyNegotiation.Commands.Create;

public class CreateProposalNegotiationResponse
{
    public Guid Id { get; set; }
    public NegotiationStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}