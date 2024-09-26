using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Realtors.Queries.Retrieve;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Realtors.Queries.RetrieveAll;

public class RetrieveAllRealtorsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<RetrieveAllRealtorsQuery, Result<RetrieveAllRealtorsResponse, Success, Error>>
{
    public async Task<Result<RetrieveAllRealtorsResponse, Success, Error>> Handle(RetrieveAllRealtorsQuery request, CancellationToken cancellationToken)
    {
        var realtors = await unitOfWork.RealtorRepository.RetrieveAllAsync();

        if (realtors == null)
        {
            return Error.NotFound;
        }

        var realtorResponses = realtors.Select(realtor => new RetrieveRealtorResponse
        {
            Id = realtor.Id,
            UserId = realtor.UserId,
            OfficeEmail = realtor.OfficeEmail,
            TotalSales = realtor.TotalSales,
            TotalListings = realtor.TotalListings,
            Certifications = realtor.Certifications,
            LanguagesSpoken = realtor.LanguagesSpoken.Select(lang => lang.ToString()).ToList()

        }).ToList();

        var response = new RetrieveAllRealtorsResponse
        { 
            Realtors = realtorResponses 
        };

        return response;
    }
}
