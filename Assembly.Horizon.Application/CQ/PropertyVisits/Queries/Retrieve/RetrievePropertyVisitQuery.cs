using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Realtors.Queries.Retrieve;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.PropertyVisits.Queries.Retrieve;

public class RetrievePropertyVisitQuery : IRequest<Result<RetrievePropertyVisitResponse, Success, Error>>
{
    public Guid UserId { get; set; }
}

public class RetrievePropertyVisitQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<RetrievePropertyVisitQuery, Result<RetrievePropertyVisitResponse, Success, Error>>
{
    public async Task<Result<RetrievePropertyVisitResponse, Success, Error>> Handle(RetrievePropertyVisitQuery request, CancellationToken cancellationToken)
    {
        var visit = await unitOfWork.PropertyVisitRepository.RetrieveByUserIdSingleAsync(request.UserId, cancellationToken);

        return new RetrievePropertyVisitResponse
        {
            Id = visit.Id,
            PropertyId = visit.PropertyId,
            UserId = visit.UserId,
            RealtorId = visit.RealtorUserId,
            VisitDate = visit.VisitDate,
            TimeSlot = visit.TimeSlot,
            Status = visit.VisitStatus,
            Notes = visit.Notes,
            PropertyTitle = visit.Property.Title,
            UserName = $"{visit.User.Name.FirstName} {visit.User.Name.LastName}".Trim(),
            RealtorName = $"{visit.RealtorUser.Name.FirstName} {visit.RealtorUser.Name.LastName}".Trim(),
        };
    }
}

public class RetrievePropertyVisitResponse
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Guid UserId { get; set; }
    public Guid RealtorId { get; set; }
    public DateOnly VisitDate { get; set; }
    public TimeSlot TimeSlot { get; set; }
    public VisitStatus Status { get; set; }
    public string Notes { get; set; }
    public string PropertyTitle { get; set; }
    public string UserName { get; set; }
    public string RealtorName { get; set; }
}