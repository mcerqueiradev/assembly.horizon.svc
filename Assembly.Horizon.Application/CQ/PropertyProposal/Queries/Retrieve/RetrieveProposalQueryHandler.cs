using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveAll;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Assembly.Horizon.Application.CQ.PropertyProposal.Queries.Retrieve;

public class RetrieveProposalQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : IRequestHandler<RetrieveProposalQuery, Result<ProposalResponse, Success, Error>>
{

    public async Task<Result<ProposalResponse, Success, Error>> Handle(RetrieveProposalQuery request, CancellationToken cancellationToken)
    {
        var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";

        var proposal = await unitOfWork.PropertyProposalRepository.RetrieveAsync(request.Id);

        if (proposal == null)
            return Error.NotFound;

        var response = new ProposalResponse
        {
            Id = proposal.Id,
            PropertyId = proposal.PropertyId,
            PropertyTitle = proposal.Property.Title,
            ProposalNumber = proposal.ProposalNumber,
            UserId = proposal.UserId,
            UserName = $"{proposal.User.Name.FirstName} {proposal.User.Name.LastName}",
            ProposedValue = proposal.ProposedValue,
            Type = proposal.Type.ToString(),
            Status = proposal.Status.ToString(),
            PaymentMethod = proposal.PaymentMethod,
            IntendedMoveDate = proposal.IntendedMoveDate,
            CreatedAt = proposal.CreatedAt,
            Images = proposal.Property?.Images?.Select(image => new PropertyImageResponse
            {
                Id = image.Id,
                FileName = image.FileName,
                ImagePath = $"{baseUrl}/uploads/{image.FileName}"
            }).ToList() ?? new List<PropertyImageResponse>()
        };

        return response;
    }
}