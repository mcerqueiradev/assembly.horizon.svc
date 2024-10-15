﻿using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Assembly.Horizon.Infra.Data.Repositories;

public class RealtorRepository : PaginatedRepository<Realtor, Guid>, IRealtorRepository
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<Realtor> _dbSet;

    public RealtorRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<Realtor>();
    }

    public async Task<Realtor> RetrieveByUserAsync(Guid id)
    {
        return await _context.Realtors.FirstOrDefaultAsync(u => u.UserId == id);
    }

    public async Task<Realtor> RetrieveByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(r => r.User)
            .ThenInclude(u => u.Account)
            .FirstOrDefaultAsync(r => r.UserId == userId, cancellationToken);
    }
}
