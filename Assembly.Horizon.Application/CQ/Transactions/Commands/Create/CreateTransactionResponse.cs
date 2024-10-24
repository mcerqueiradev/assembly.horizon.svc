using Assembly.Horizon.Domain.Model;

namespace Assembly.Horizon.Application.CQ.Transactions.Commands.Create;

public class CreateTransactionResponse
{
    public Guid Id { get; set; }
    public Guid ContractId { get; set; }
    public Guid InvoiceId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public string PaymentMethod { get; set; }
    public Transaction.TransactionStatus Status { get; set; }
}