using Assembly.Horizon.Domain.Common;
using Assembly.Horizon.Domain.Interface;

namespace Assembly.Horizon.Domain.Model;

public class Contract : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Property Property { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid RealtorId { get; set; }
    public Realtor Realtor { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double Value { get; set; }
    public string TermsAndConditions { get; set; }
    public double AdditionalFees { get; set; }
    public string PaymentFrequency { get; set; }
    public bool RenewalOption { get; set; }
    public string TerminationClauses { get; set; }
    public bool IsActive { get; set; }
    public DateTime? LastActiveDate { get; set; }

    private Contract()
    {
        Id = Guid.NewGuid();
    }
    public Contract(
        Guid id,
        Guid propertyId,
        Guid userId,
        DateTime startDate,
        DateTime endDate,
        double value,
        string termsAndConditions,
        double additionalFees,
        string paymentFrequency,
        bool renewalOption,
        string terminationClauses,
        bool isActive,
        DateTime? lastActiveDate)
    {
        Id = id;
        PropertyId = propertyId;
        UserId = userId;
        StartDate = startDate;
        EndDate = endDate;
        Value = value;
        TermsAndConditions = termsAndConditions;
        AdditionalFees = additionalFees;
        PaymentFrequency = paymentFrequency;
        RenewalOption = renewalOption;
        TerminationClauses = terminationClauses;
        IsActive = isActive;
        LastActiveDate = lastActiveDate;
    }
}
