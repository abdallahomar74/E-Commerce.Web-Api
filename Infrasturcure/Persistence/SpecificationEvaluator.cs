using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    static class SpecificationEvaluator 
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey> (IQueryable<TEntity> inputQuery , ISpecifications<TEntity,TKey>  specifications) where TEntity : BaseEntity<TKey>
        {
            var Query = inputQuery;
            if (specifications.criteria is not null)
            {
                Query = Query.Where (specifications.criteria);
            }
            if (specifications.OrderBy is not null)
            {
                Query = Query.OrderBy(specifications.OrderBy);
            }
            if (specifications.OrderByDescending is not null)
            {
                Query = Query.OrderByDescending(specifications.OrderByDescending);
            }
            if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Count > 0)
            {
                Query = specifications.IncludeExpressions.Aggregate(Query, (CurrnetQuery, IncludeExp) => CurrnetQuery.Include(IncludeExp));
            }
            if (specifications.IsPaginated)
            {
                Query = Query.Skip(specifications.Skip).Take(specifications.Take);
            }
            return Query;
        }
    }
}
