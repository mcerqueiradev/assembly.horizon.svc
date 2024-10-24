using Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveAll;
using Assembly.Horizon.Domain.Model;

namespace Assembly.Horizon.Application.CQ.Contracts.Queries.Retrieve;

public class RetrieveContractResponse
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid RealtorId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double Value { get; set; }
    public double AdditionalFees { get; set; }
    public string PaymentFrequency { get; set; }
    public bool RenewalOption { get; set; }
    public bool IsActive { get; set; }
    public DateTime? LastActiveDate { get; set; }
    public string ContractType { get; set; }
    public string Status { get; set; }
    public DateTime SignatureDate { get; set; }
    public decimal? SecurityDeposit { get; set; }
    public string InsuranceDetails { get; set; }
    public string Notes { get; set; }
    public int DurationInMonths { get; set; }
    public string DocumentPath { get; set; } 
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerPhoto {  get; set; }
    public string RealtorName { get; set; }
    public string RealtorEmail { get; set; }
    public string RealtorPhoto { get; set; }

    // Property Props
    public string PropertyTitle { get; set; }
    public string PropertyStreet { get; set; }
    public string PropertyCity { get; set; }
    public string PropertyState { get; set; }
    public string PropertyPostalCode { get; set; }
    public string PropertyCountry { get; set; }

    public double PropertySize { get; set; }
    public int PropertyBedrooms { get; set; }
    public int PropertyBathrooms { get; set; }
    public List<PropertyImageResponse> Images { get; set; }
}