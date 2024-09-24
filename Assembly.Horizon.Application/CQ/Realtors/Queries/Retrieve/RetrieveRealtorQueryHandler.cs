using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Realtors.Queries.Retrieve;

public class RetrieveRealtorQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<RetrieveRealtorQuery, Result<RetrieveRealtorResponse, Success, Error>>
{
    public async Task<Result<RetrieveRealtorResponse, Success, Error>> Handle(RetrieveRealtorQuery request, CancellationToken cancellationToken)
    {
        var realtor = await unitOfWork.RealtorRepository.RetrieveAsync(request.Id);

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
