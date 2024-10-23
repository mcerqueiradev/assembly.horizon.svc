using Assembly.Horizon.Domain.Common;
using Assembly.Horizon.Domain.Interface;
using System.Transactions;

namespace Assembly.Horizon.Domain.Model;

public class Transaction : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid ContractId { get; set; }
    public Contract Contract { get; set; }
    public Guid InvoiceId { get; set; }
    public Invoice Invoice { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public string PaymentMethod { get; set; }
    public TransactionStatus Status { get; set; }
    public string TransactionHistory { get; set; }

    protected Transaction()
    {
        Id = Guid.NewGuid();
    }

    public Transaction(
               Guid contractId,
               Guid invoiceId,
               decimal amount,
               DateTime date,
               string description,
               string paymentMethod,
               TransactionStatus status,
               string transactionHistory)
    {
        Id = Guid.NewGuid();
        ContractId = contractId;
        InvoiceId = invoiceId;
        Amount = amount;
        Date = date;
        Description = description;
        PaymentMethod = paymentMethod;
        Status = status;
        TransactionHistory = transactionHistory;
    }

    public enum TransactionStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded
    }
}