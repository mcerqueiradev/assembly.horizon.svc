﻿using Assembly.Horizon.Domain.Core.Interfaces;
using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Assembly.Horizon.Infra.Data.Repositories;

public class PropertyRepository : PaginatedRepository<Property, Guid>, IPropertyRepository
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<Property> _dbSet;

    public PropertyRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<Property>();
    }
}
