using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.PropertyVisits.Commands.Create;

public class CreatePropertyVisitCommand : IRequest<Result<CreatePropertyVisitResponse, Success, Error>>
{
    public Guid PropertyId { get; set; }
    public Guid UserId { get; set; }
    public Guid RealtorId { get; set; }
    public DateOnly VisitDate { get; set; }
    public TimeSlot TimeSlot { get; set; }
    public string? Notes { get; set; }
}