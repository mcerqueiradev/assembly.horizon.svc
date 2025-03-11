using Assembly.Horizon.Domain.Model;

namespace Assembly.Horizon.Application.CQ.Contracts.Commands.Create;

public class CreateContractResponse
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid RealtorId { get; set; }
    public Guid? ProposalId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Value { get; set; }
    public bool IsActive { get; set; }
    public ContractType ContractType { get; set; }
    public ContractStatus Status { get; set; }
    public DateTime SignatureDate { get; set; }
    public string DocumentPath { get; set; }
    public int DurationInMonths { get; set; }
}