using Assembly.Horizon.Domain.Common;
using Assembly.Horizon.Domain.Interface;
using System.Globalization;

namespace Assembly.Horizon.Domain.Model;

public enum ContractType
{
    Sale,
    Rent,
}

public enum ContractStatus
{
    Draft,
    Pending,
    Active,
    Completed,
    Terminated,
    Expired
}

public class Contract : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Property Property { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }
    public Guid RealtorId { get; set; }
    public Realtor Realtor { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Value { get; set; }
    public decimal AdditionalFees { get; set; }
    public string PaymentFrequency { get; set; }
    public bool RenewalOption { get; set; }
    public bool IsActive { get; set; }
    public DateTime? LastActiveDate { get; set; }
    public ContractType ContractType { get; set; }
    public ContractStatus Status { get; set; }
    public DateTime SignatureDate { get; set; }
    public decimal? SecurityDeposit { get; set; }
    public string? InsuranceDetails { get; set; }
    public string? Notes { get; set; }
    public Guid? ProposalId { get; set; }
    public PropertyProposal? Proposal { get; set; }
    public string DocumentPath { get; set; }
    public ICollection<Invoice> Invoices { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
    public int DurationInMonths { get; set; }
    public string ContractName { get; private set; }

    public Contract()
    {
        Id = Guid.NewGuid();
        Invoices = new List<Invoice>();
        Transactions = new List<Transaction>();
        ContractName = GenerateContractName();
    }

    public Contract(
        Guid id,
        Guid propertyId,
        Guid customerId,
        Guid realtorId,
        DateTime startDate,
        DateTime endDate,
        decimal value,
        decimal additionalFees,
        string paymentFrequency,
        bool renewalOption,
        bool isActive,
        DateTime? lastActiveDate,
        ContractType contractType,
        ContractStatus status,
        DateTime signatureDate,
        decimal? securityDeposit,
        string insuranceDetails,
        string notes,
        string documentPath)
    {
        Id = id;
        PropertyId = propertyId;
        CustomerId = customerId;
        RealtorId = realtorId;
        StartDate = startDate;
        EndDate = endDate;
        Value = value;
        AdditionalFees = additionalFees;
        PaymentFrequency = paymentFrequency;
        RenewalOption = renewalOption;
        IsActive = isActive;
        LastActiveDate = lastActiveDate;
        ContractType = contractType;
        Status = status;
        SignatureDate = signatureDate;
        SecurityDeposit = securityDeposit;
        InsuranceDetails = insuranceDetails;
        Notes = notes;
        DocumentPath = documentPath;
        Invoices = new List<Invoice>();
        Transactions = new List<Transaction>();
        CalculateDurationInMonths();
    }

    private string GenerateContractName()
    {
        var year = DateTime.UtcNow.Year;
        var randomNumber = Random.Shared.Next(1000, 9999).ToString();
        var contractType = ContractType.ToString().Substring(0, 1);

        return $"CTR-{year}-{randomNumber}-{contractType}";
    }

    private void CalculateDurationInMonths()
    {
        DurationInMonths = ((EndDate.Year - StartDate.Year) * 12) + EndDate.Month - StartDate.Month;
        if (EndDate.Day < StartDate.Day)
        {
            DurationInMonths--;
        }
    }

    public void UpdateDates(DateTime newStartDate, DateTime newEndDate)
    {
        StartDate = newStartDate;
        EndDate = newEndDate;
        CalculateDurationInMonths();
    }

    public void GenerateTransactions()
    {
        if (ContractType == ContractType.Rent)
        {
            GenerateRentTransactions();
        }
        else if (ContractType == ContractType.Sale)
        {
            GenerateSaleTransaction();
        }
    }

    private void GenerateRentTransactions()
    {
        var frequency = PaymentFrequency.ToLower();
        var monthsInterval = frequency switch
        {
            "monthly" => 1,
            "quarterly" => 3,
            "semi-annually" => 6,
            "annually" => 12,
            _ => 1
        };

        for (int i = 0; i < DurationInMonths; i += monthsInterval)
        {
            var dueDate = StartDate.AddMonths(i);
            var description = $"Rent payment for {dueDate.ToString("MMMM yyyy", new CultureInfo("en-US"))}";

            var transaction = new Transaction(
                Id,                     // ContractId
                Guid.Empty,             // Temporary InvoiceId
                Customer.UserId,        // UserId
                Value,                  // Amount
                dueDate,               // Date
                description,           // Description
                "bank_transfer",       // PaymentMethod
                Transaction.TransactionStatus.Pending  // Status
            );

            var invoice = transaction.GenerateInvoice();
            Invoices.Add(invoice);
            Transactions.Add(transaction);
        }
    }

    private void GenerateSaleTransaction()
    {
        var description = $"Property purchase payment - {StartDate.ToString("MMMM yyyy", new CultureInfo("en-US"))}";

        var transaction = new Transaction(
            Id,
            Guid.Empty,
            Customer.UserId,
            Value + AdditionalFees,
            StartDate,
            description,
            "bank_transfer",
            Transaction.TransactionStatus.Pending
        );

        var invoice = transaction.GenerateInvoice();
        Invoices.Add(invoice);
        Transactions.Add(transaction);
    }

}
