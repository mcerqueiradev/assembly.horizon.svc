using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Horizon.Domain.Core.Interfaces;

public interface IAccountRepository : IRepository<Account, Guid>
{
    Task<Account> GetByEmailAsync(string email);
}
