namespace Assembly.Horizon.Infra.Data.Infrastructure.Models;

public class VisitConfirmedEmail
{
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string PropertyTitle { get; set; }
    public DateOnly VisitDate { get; set; }
    public string TimeSlot { get; set; }
    public string RealtorName { get; set; }
    public string RealtorEmail { get; set; }
    public string RealtorPhone { get; set; }
}
