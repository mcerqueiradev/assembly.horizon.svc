using Assembly.Horizon.Domain.Model;

namespace Assembly.Horizon.Infra.Data.Infrastructure.Models;

public class VisitScheduledEmail
{
    public string PropertyTitle { get; set; }
    public DateOnly VisitDate { get; set; }
    public TimeSlot TimeSlot { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string? PhoneNumber { get; set; }
    public string RealtorName { get; set; }
    public string RealtorEmail { get; set; }
    public string PropertyAddress { get; set; }
    public string Notes { get; set; }
    public string ConfirmationToken { get; set; }
    }
