namespace Assembly.Horizon.Infra.Data.Infrastructure.Models;

public class ProposalSubmittedEmail
{
    public string PropertyTitle { get; set; }
    public decimal ProposedValue { get; set; }
    public string PaymentMethod { get; set; }
    public DateTime IntendedMoveDate { get; set; }
    public string ProposalType { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string PhoneNumber { get; set; }
    public string RealtorName { get; set; }
    public string RealtorEmail { get; set; }
    public string PropertyAddress { get; set; }
}
