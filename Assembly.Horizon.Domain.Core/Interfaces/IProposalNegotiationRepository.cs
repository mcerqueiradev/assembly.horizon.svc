using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Interface.Repositories;

namespace Assembly.Horizon.Domain.Core.Interfaces;

public interface IProposalNegotiationRepository : IRepository<ProposalNegotiation, Guid>
{
    Task<List<ProposalNegotiation>> ListByProposalAsync(Guid proposalId);

    Task<List<ProposalNegotiation>> ListByProposalWithSenderAsync(Guid proposalId);
}
