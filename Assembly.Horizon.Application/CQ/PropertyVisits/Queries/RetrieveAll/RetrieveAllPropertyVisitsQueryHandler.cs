using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.PropertyVisits.Queries.Retrieve;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Assembly.Horizon.Application.CQ.PropertyVisits.Queries.RetrieveAll;

public class RetrieveAllPropertyVisitsQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : IRequestHandler<RetrieveAllPropertyVisitsQuery, Result<RetrieveAllPropertyVisitsResponse, Success, Error>>
{
    public async Task<Result<RetrieveAllPropertyVisitsResponse, Success, Error>> Handle(RetrieveAllPropertyVisitsQuery request, CancellationToken cancellationToken)
    {

        var visits = await unitOfWork.PropertyVisitRepository.RetrieveAllAsync(cancellationToken);

        if (visits == null || !visits.Any())
            return Error.NotFound;

        var visitsResponses = visits.Select(visit => new RetrievePropertyVisitResponse
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
        }).ToList();

        var response = new RetrieveAllPropertyVisitsResponse { Visits = visitsResponses };

        return response;
    }
}
