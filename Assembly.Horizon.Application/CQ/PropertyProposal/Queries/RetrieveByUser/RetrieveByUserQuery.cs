using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Properties.Queries.RetrieveAll;
using Assembly.Horizon.Application.CQ.PropertyProposal.Queries.Retrieve;
using Assembly.Horizon.Application.CQ.Users.Queries.Retrieve;
using Assembly.Horizon.Domain.Core.Uow;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Horizon.Application.CQ.PropertyProposal.Queries.RetrieveByUser;

public class RetrieveByUserQuery : IRequest<Result<RetrieveByUserResponse, Success, Error>>
{
    public Guid UserId { get; set; }
}

    public class RetrieveByUserResponse
{
    public IEnumerable<ProposalResponse> Proposals { get; set; }
}

public class RetrieveByUserQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : IRequestHandler<RetrieveByUserQuery, Result<RetrieveByUserResponse, Success, Error>>
{
    public async Task<Result<RetrieveByUserResponse, Success, Error>> Handle(RetrieveByUserQuery request, CancellationToken cancellationToken)
    {
        var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";

        var proposals = await unitOfWork.PropertyProposalRepository.RetrieveByUserAsync(request.UserId);

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

        var response = new RetrieveByUserResponse
        {
            Proposals = proposalResponses
        };

        return response;
    }
}