using SimpleWebShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SimpleWebShop.Domain.UnitOfWorks
{
    // GENERIC SPECIFICATION IMPLEMENTATION (BASE CLASS)
    // https://github.com/dotnet-architecture/eShopOnWeb

    public class Specification<TEntity>
        : ISpecification<TEntity> where TEntity : Entity
    {

        public Specification()
        { }

        public IReadOnlyCollection<Expression<Func<TEntity, object>>> Includes
            => _includes;
        protected List<Expression<Func<TEntity, object>>> _includes { get; } =
            new List<Expression<Func<TEntity, object>>>();

        public IReadOnlyCollection<string> IncludeStrings => _includeStrings;
        protected List<string> _includeStrings { get; } = new List<string>();

        public virtual void Include(
            Expression<Func<TEntity, object>> includeExpression)
        {
            _includes.Add(includeExpression);
        }

        // string-based includes allow for including children of children
        // e.g. Basket.Items.Product
        public virtual void Include(string includeString)
        {
            _includeStrings.Add(includeString);
        }
    }
}
