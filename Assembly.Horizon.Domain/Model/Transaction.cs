using Assembly.Horizon.Domain.Common;
using Assembly.Horizon.Domain.Interface;
using System.Transactions;

namespace Assembly.Horizon.Domain.Model;

public class Transaction : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid ContractId { get; set; }
    public Contract Contract { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid InvoiceId { get; set; }
    public Invoice Invoice { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public string PaymentMethod { get; set; }
    public TransactionStatus Status { get; set; }

    protected Transaction()
    {
        Id = Guid.NewGuid();
    }

    public Transaction(
               Guid contractId,
               Guid invoiceId,
               Guid userId,
               decimal amount,
               DateTime date,
               string description,
               string paymentMethod,
               TransactionStatus status)
    {
        Id = Guid.NewGuid();
        ContractId = contractId;
        InvoiceId = invoiceId;
        UserId = userId;
        Amount = amount;
        Date = date;
        Description = description;
        PaymentMethod = paymentMethod;
        Status = status;
    }

    public Invoice GenerateInvoice()
    {
        var uniquePart = Guid.NewGuid().ToString().Substring(0, 6);
        var invoiceNumber = $"INV#{Date:MM-yyyy}-{uniquePart}";

        var invoice = new Invoice(
            ContractId,
            invoiceNumber,
            Amount,
            Date,
            Date.AddDays(30), // Assuming a 30-day due period
            InvoiceStatus.Paid // Since this is generated from a transaction, it's already paid
        );

        InvoiceId = invoice.Id;
        Invoice = invoice;

        return invoice;
    }

    public enum TransactionStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded
    }
}