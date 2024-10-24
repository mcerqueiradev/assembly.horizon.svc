using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Interfaces;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Transactions.Commands.Create;

public class CreateTransactionCommand : IRequest<Result<CreateTransactionResponse, Success, Error>>
{
    public Guid ContractId { get; init; }
    public Guid UserId { get; set; }
    public decimal Amount { get; init; }
    public string Description { get; init; }
    public string PaymentMethod { get; init; }
}
