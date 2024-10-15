using Assembly.Horizon.Domain.Model;

namespace Assembly.Horizon.Application.CQ.Contracts.Commands.Create;

public class CreateContractResponse
{
    public Guid Id { get; init; }
    public Guid PropertyId { get; init; }
    public Guid CustomerId { get; init; }
    public Guid RealtorId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public double Value { get; init; }
    public bool IsActive { get; init; }
    public ContractType ContractType { get; init; }
    public ContractStatus Status { get; init; }
    public DateTime SignatureDate { get; init; }
    public string DocumentPath { get; init; }
}