using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Atfal360.Context;
using Atfal360.Contracts.Repositories;
using Atfal360.Entities;
using Atfal360.Paging;
using Microsoft.EntityFrameworkCore;

namespace Atfal360.Implementations.Repositories
{
    public class AtfalRepository : IAtfalRepository
    {
        private readonly ApplicationDbContext _context;
        public AtfalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Atfal> Create(Atfal atfal)
        {
            await _context.Atfals.AddAsync(atfal);
            await _context.SaveChangesAsync();
            return atfal;
        }

        public async Task<PaginatedList<Atfal>> Get(Expression<Func<Atfal, bool>> expression, PageRequest request)
        {
            var query = _context.Atfals.Where(expression).OrderBy(r => r.Name);
            var totalItemsCount = await query.CountAsync();
            if (request.UsePaging)
            {
                var offset = (request.Page - 1) * request.PageSize;
                var result = await query.Skip(offset).Take(request.PageSize).ToListAsync();
                return result.ToPaginatedList(totalItemsCount, request.Page, request.PageSize);
            }
            else
            {
                var result = await query.ToListAsync();
                return result.ToPaginatedList(totalItemsCount, 1, totalItemsCount);
            }

        }

        public IQueryable<Atfal> GetAtfals()
        {
            return _context.Atfals.AsQueryable();
        }
    }
}