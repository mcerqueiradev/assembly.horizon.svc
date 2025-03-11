using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Realtors.Queries.Retrieve;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Realtors.Queries.RetrieveByUserId;

public class RetrieveRealtorByUserIdQuery : IRequest<Result<RetrieveRealtorResponse, Success, Error>>
{
    public Guid UserId { get; set; }
}

public class RetrieveRealtorByUserIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<RetrieveRealtorByUserIdQuery, Result<RetrieveRealtorResponse, Success, Error>>
{
    public async Task<Result<RetrieveRealtorResponse, Success, Error>> Handle(RetrieveRealtorByUserIdQuery request, CancellationToken cancellationToken)
    {
        var realtor = await unitOfWork.RealtorRepository.RetrieveByUserIdAsync(request.UserId);

        if (realtor == null)
        {
            return Error.NotFound;
        }

        var response = new RetrieveRealtorResponse()
        {
            Id = realtor.Id,
            UserId = realtor.UserId,
            OfficeEmail = realtor.OfficeEmail,
            TotalSales = realtor.TotalSales,
            TotalListings = realtor.TotalListings,
            Certifications = realtor.Certifications,
            LanguagesSpoken = realtor.LanguagesSpoken.Select(lang => lang.ToString()).ToList()
        };

        return response;
    }
}
