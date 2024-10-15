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
    public string TemplateVersion { get; set; }

    public Contract()
    {
        Id = Guid.NewGuid();
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
        string documentPath,
        string templateVersion)
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
        TemplateVersion = templateVersion;
    }
}

public enum ContractType
{
    Sale,
    Rent,
    LeaseToOwn
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