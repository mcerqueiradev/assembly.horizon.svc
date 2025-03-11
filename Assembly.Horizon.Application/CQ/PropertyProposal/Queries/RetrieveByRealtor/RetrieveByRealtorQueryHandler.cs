using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveAll;
using Assembly.Horizon.Application.CQ.PropertyProposal.Queries.Retrieve;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Assembly.Horizon.Application.CQ.PropertyProposal.Queries.RetrieveByRealtor;

public class RetrieveByRealtorQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<RetrieveByRealtorQuery, Result<RetrieveByRealtorResponse, Success, Error>>
{
    public async Task<Result<RetrieveByRealtorResponse, Success, Error>> Handle(RetrieveByRealtorQuery request, CancellationToken cancellationToken)
    {
        var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";

        var properties = await unitOfWork.PropertyRepository.RetrieveByRealtorAsync(request.RealtorId);
        var propertyIds = properties.Select(p => p.Id);

        var proposals = await unitOfWork.PropertyProposalRepository.RetrieveByPropertiesAsync(propertyIds);

        var proposalResponses = proposals.Select(p => new ProposalResponse
        {
            Id = p.Id,
            PropertyId = p.PropertyId,
            PropertyTitle = p.Property.Title,
            ProposalNumber = p.ProposalNumber,
            UserId = p.UserId,
            UserName = $"{p.User.Name.FirstName} {p.User.Name.LastName}",
            ProposedValue = p.ProposedValue,
            Type = p.Type.ToString(),
            Status = p.Status.ToString(),
            PaymentMethod = p.PaymentMethod,
            IntendedMoveDate = p.IntendedMoveDate,
            CreatedAt = p.CreatedAt,
            Images = p.Property?.Images?.Select(image => new PropertyImageResponse
            {
                Id = image.Id,
                FileName = image.FileName,
                ImagePath = $"{baseUrl}/uploads/{image.FileName}"
            }).ToList() ?? new List<PropertyImageResponse>()
        });

        var response = new RetrieveByRealtorResponse
        {
            Proposals = proposalResponses
        };

        return response;
    }
}
