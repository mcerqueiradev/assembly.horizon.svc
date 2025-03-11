using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Interface.Repositories;

namespace Assembly.Horizon.Domain.Core.Interfaces;

public interface IPropertyProposalRepository : IRepository<PropertyProposal, Guid>
{
    Task<List<PropertyProposal>> RetrieveByPropertiesAsync(IEnumerable<Guid> propertyIds, CancellationToken cancellationToken = default);
    Task<IEnumerable<PropertyProposal>> RetrieveByUserAsync(Guid userId);

    Task UpdateStatusAsync(Guid proposalId, ProposalStatus status, CancellationToken cancellationToken);
}
