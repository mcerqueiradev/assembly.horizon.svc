namespace Assembly.Horizon.Application.CQ.Transactions.Queries.Retrieve;

public record TransactionResponse(
Guid Id,
Guid ContractId,
Guid InvoiceId,
Guid UserId,
DateTime Date,
string Description,
string Status,
string PaymentMethod,
decimal Amount,
DateTime CreatedAt,
string TransactionNumber
);
