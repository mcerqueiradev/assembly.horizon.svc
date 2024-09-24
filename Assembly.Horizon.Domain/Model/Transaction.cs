using Assembly.Horizon.Domain.Common;
using Assembly.Horizon.Domain.Interface;

namespace Assembly.Horizon.Domain.Model;

public class Transaction : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }
    public Guid PropertyId { get; set; }
    public Property Property { get; set; }
    public Guid RealtorId { get; set; }
    public Realtor Realtor { get; set; }
    public double Value { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public int Invoice { get; set; }
    public string PaymentMethod { get; set; }
    public string TransactionStatus { get; set; }
    public string TransactionHistory { get; set; } 
    public bool IsBuyer { get; set; }
    public bool IsTenant { get; set; }

    // Construtor protegido para garantir a criação com um GUID
    protected Transaction()
    {
        Id = Guid.NewGuid();
    }

    // Construtor completo com todas as dependências
    public Transaction(
        Guid customerId,
        Guid propertyId,
        Guid realtorId,
        double value,
        DateTime date,
        string description,
        int invoice,
        string paymentMethod,
        string transactionStatus,
        string transactionHistory,
        bool isBuyer,
        bool isTenant)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        PropertyId = propertyId;
        RealtorId = realtorId;
        Value = value;
        Date = date;
        Description = description;
        Invoice = invoice;
        PaymentMethod = paymentMethod;
        TransactionStatus = transactionStatus;
        TransactionHistory = transactionHistory;
        IsBuyer = isBuyer;
        IsTenant = isTenant;
    }
}