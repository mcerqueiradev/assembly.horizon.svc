using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Interface.Repositories;
using System.Diagnostics.Contracts;
using Contract = Assembly.Horizon.Domain.Model.Contract;

namespace Assembly.Horizon.Domain.Core.Interfaces;

public interface IContractRepository : IRepository<Contract, Guid>
{
}
