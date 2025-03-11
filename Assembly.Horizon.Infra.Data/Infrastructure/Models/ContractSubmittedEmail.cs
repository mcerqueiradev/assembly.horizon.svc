namespace Assembly.Horizon.Infra.Data.Infrastructure.Models;

public class ContractSubmittedEmail
{
    public string PropertyTitle { get; set; }
    public decimal ContractValue { get; set; }
    public string PaymentFrequency { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string ContractType { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string RealtorName { get; set; }
    public string RealtorEmail { get; set; }
    public string PropertyAddress { get; set; }
    public decimal SecurityDeposit { get; set; }
    public string DocumentPath { get; set; }
}