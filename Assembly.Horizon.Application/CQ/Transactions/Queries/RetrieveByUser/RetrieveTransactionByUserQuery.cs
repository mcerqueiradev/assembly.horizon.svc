using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Transactions.Queries.Retrieve;
using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Transactions.Queries.RetrieveByUser;

public record RetrieveTransactionByUserQuery(Guid UserId)
    : IRequest<Result<RetrieveTransactionByUserResponse, Success, Error>>;

public class RetrieveTransactionByUserQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<RetrieveTransactionByUserQuery, Result<RetrieveTransactionByUserResponse, Success, Error>>
{
    public async Task<Result<RetrieveTransactionByUserResponse, Success, Error>> Handle(
        RetrieveTransactionByUserQuery request,
        CancellationToken cancellationToken)
    {
        var transactions = await unitOfWork.TransactionRepository.GetByUserIdAsync(request.UserId);

        if (!transactions.Any())
            return Error.NotFound;

        var transactionResponses = transactions.Select(t => new TransactionResponse(
            t.Id,
            t.ContractId,
            t.InvoiceId,
            t.Date,
            t.Description,
            t.Status.ToString(),
            t.PaymentMethod,
            t.Amount,
            t.CreatedAt
        )).ToList();

        return new RetrieveTransactionByUserResponse(transactionResponses);
    }
}

public record RetrieveTransactionByUserResponse(
    List<TransactionResponse> Transactions
);