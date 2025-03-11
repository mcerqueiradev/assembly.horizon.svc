using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Contracts.Commands.Create;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Contracts.Commands.CreateFromProposal;

public record CreateContractFromProposalCommand : IRequest<Result<CreateContractResponse, Success, Error>>
{
    public Guid ProposalId { get; init; }
    public Guid PropertyId { get; init; }
    public Guid CustomerId { get; init; }
    public Guid RealtorId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public decimal Value { get; init; }
    public decimal AdditionalFees { get; init; }
    public string PaymentFrequency { get; init; }
    public bool RenewalOption { get; init; }
    public bool IsActive { get; init; }
    public DateTime? LastActiveDate { get; init; }
    public ContractType ContractType { get; init; }
    public ContractStatus Status { get; init; }
    public DateTime SignatureDate { get; init; }
    public decimal? SecurityDeposit { get; init; }
    public string InsuranceDetails { get; init; }
    public string Notes { get; init; }
    public int DurationInMonths { get; init; }
}
