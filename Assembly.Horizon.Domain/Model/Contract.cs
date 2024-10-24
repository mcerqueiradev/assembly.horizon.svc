using Assembly.Horizon.Domain.Common;
using Assembly.Horizon.Domain.Interface;

namespace Assembly.Horizon.Domain.Model;

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
    public double Value { get; set; }
    public double AdditionalFees { get; set; }
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
    public string DocumentPath { get; set; }
    public ICollection<Invoice> Invoices { get; set; }
    public ICollection<Transaction> Transactions { get; set; }

    public int DurationInMonths { get; set; }

    public Contract()
    {
        Id = Guid.NewGuid();
        Invoices = new List<Invoice>();
        Transactions = new List<Transaction>();
    }

    public Contract(
        Guid id,
        Guid propertyId,
        Guid customerId,
        Guid realtorId,
        DateTime startDate,
        DateTime endDate,
        double value,
        double additionalFees,
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

    // Método para calcular a duração em meses
    private void CalculateDurationInMonths()
    {
        DurationInMonths = ((EndDate.Year - StartDate.Year) * 12) + EndDate.Month - StartDate.Month;

        // Ajuste para casos onde o dia final é anterior ao dia inicial
        if (EndDate.Day < StartDate.Day)
        {
            DurationInMonths--;
        }
    }

    // Método para recalcular a duração se as datas forem alteradas
    public void UpdateDates(DateTime newStartDate, DateTime newEndDate)
    {
        StartDate = newStartDate;
        EndDate = newEndDate;
        CalculateDurationInMonths();
    }

    public void GenerateInvoices()
    {
        if (ContractType == ContractType.Rent)
        {
            for (int i = 0; i < DurationInMonths; i++)
            {
                var dueDate = StartDate.AddMonths(i);
                var uniquePart = Guid.NewGuid().ToString().Substring(0, 6); // A unique 6-character part
                var invoiceNumber = $"INV#{dueDate:MM-yyyy}-{i + 1}-{uniquePart}"; // Example: INV#05-2024-1-ABC123
                var invoice = new Invoice(
                    Id,
                    invoiceNumber,
                    (decimal)Value,
                    dueDate.AddDays(-7),
                    dueDate,
                    InvoiceStatus.Pending
                );
                Invoices.Add(invoice);
            }
        }
        else if (ContractType == ContractType.Sale)
        {
            var uniquePart = Guid.NewGuid().ToString().Substring(0, 6); // A unique 6-character part
            var invoiceNumber = $"INV#{StartDate:MM-yyyy}-{uniquePart}"; // Example: INV#05-2024-ABC123 for a sale in May 2024
            var invoice = new Invoice(
                Id,
                invoiceNumber,
                (decimal)(Value + AdditionalFees),
                StartDate,
                StartDate.AddDays(30),
                InvoiceStatus.Pending
            );
            Invoices.Add(invoice);
        }
    }
}

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