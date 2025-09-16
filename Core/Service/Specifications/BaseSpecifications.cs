using DomainLayer.Contracts;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    internal class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteriaExpression )
        {criteria = criteriaExpression; }
        
        public Expression<Func<TEntity, bool>>? criteria { get; private set; }

        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpressions)
            => IncludeExpressions.Add(includeExpressions);
        #endregion
        #region Sorting
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

  
        protected void AddOrderBy(Expression<Func<TEntity, object>> OrderByExpression) => OrderBy = OrderByExpression;
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> OrderByDescExpression) => OrderByDescending = OrderByDescExpression;
        #endregion
        #region Pagination
        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPaginated { get ; set ; }
        public void ApplyPagination (int PageIndex, int PageSize)
        {
            IsPaginated = true;
            Take = PageSize;
            Skip = (PageIndex - 1) * PageSize;
        }

        #endregion

    }
}
