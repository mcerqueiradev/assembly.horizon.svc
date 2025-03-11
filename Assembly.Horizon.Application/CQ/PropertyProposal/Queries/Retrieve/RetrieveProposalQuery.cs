using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.PropertyProposal.Queries.Retrieve;

public record RetrieveProposalQuery(Guid Id) : IRequest<Result<ProposalResponse, Success, Error>>;
