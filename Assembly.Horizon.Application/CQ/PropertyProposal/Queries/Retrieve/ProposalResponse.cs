using Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveAll;

namespace Assembly.Horizon.Application.CQ.PropertyProposal.Queries.Retrieve;

public class ProposalResponse
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public string ProposalNumber { get; set; }
    public string PropertyTitle { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public decimal ProposedValue { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
    public string PaymentMethod { get; set; }
    public DateTime IntendedMoveDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<PropertyImageResponse> Images { get; set; }
}
