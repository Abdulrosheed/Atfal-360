using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Atfal360.Entities;
using Atfal360.Paging;

namespace Atfal360.Contracts.Repositories
{
    public interface IAtfalRepository
    {
        Task<Atfal> Create (Atfal atfal);
        Task<PaginatedList<Atfal>> Get (Expression<Func<Atfal, bool>> expression , PageRequest request);
        IQueryable<Atfal> GetAtfals();
    }
}