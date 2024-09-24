using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Domain.Core.Uow;
using Assembly.Horizon.Domain.Model;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Realtors.Command.Create;

public class CreateRealtorCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateRealtorCommand, Result<CreateRealtorResponse, Success, Error>>
{

    public async Task<Result<CreateRealtorResponse, Success, Error>> Handle(CreateRealtorCommand request, CancellationToken cancellationToken)
    {
        var languagesSpoken = new List<Languages>();
        foreach( var language in request.LanguagesSpoken)
        {
            if (Enum.TryParse<Languages>(language, out var parsedLanguage))
            {
                languagesSpoken.Add(parsedLanguage);
            }
            else
            {
                return Error.NotFound;
            }
        }

        var realtor = new Realtor
        {
            UserId = request.UserId,
            OfficeEmail = request.OfficeEmail,
            TotalSales = request.TotalSales,
            TotalListings = request.TotalListings,
            Certifications = request.Certifications,
            LanguagesSpoken = languagesSpoken 
        };

        await unitOfWork.RealtorRepository.AddAsync(realtor, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        var updateAccess = await unitOfWork.UserRepository.RetrieveAsync(request.UserId, cancellationToken);
        if (updateAccess == null)
        {
            return Error.NotFound;
        }

        updateAccess.Access = Access.Realtor;

        await unitOfWork.UserRepository.UpdateAsync(updateAccess, cancellationToken);  
        await unitOfWork.CommitAsync(cancellationToken);

        var response = new CreateRealtorResponse
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
