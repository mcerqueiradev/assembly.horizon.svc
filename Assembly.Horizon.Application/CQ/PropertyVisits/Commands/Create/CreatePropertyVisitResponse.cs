using Assembly.Horizon.Domain.Model;

namespace Assembly.Horizon.Application.CQ.PropertyVisits.Commands.Create;

public class CreatePropertyVisitResponse
{
    public Guid Id { get; set; }
    public DateOnly VisitDate { get; set; }
    public TimeSlot TimeSlot { get; set; }
    public VisitStatus Status { get; set; }
}