﻿using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Interface.Repositories;

namespace Assembly.Horizon.Domain.Core.Interfaces;

public interface IProposalDocumentRepository : IRepository<ProposalDocument, Guid>
{
}
