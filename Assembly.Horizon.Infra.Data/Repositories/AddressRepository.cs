using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Assembly.Horizon.Infra.Data.Repositories;

public class AddressRepository : PaginatedRepository<Address, Guid>, IAddressRepository
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<Address> _dbSet;

    public AddressRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<Address>();
    }

    public async Task<Address> GetFullAddressAsync(Address address, CancellationToken cancellationToken)
    {

        return await _context.Addresses
            .Where(a => a.Street == address.Street &&
                        a.City == address.City &&
                        a.State == address.State &&
                        a.PostalCode == address.PostalCode &&
                        a.Country == address.Country &&
                        a.Reference == address.Reference)
            .FirstOrDefaultAsync(cancellationToken);
    }

}
