using SimpleWebShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SimpleWebShop.Domain.UnitOfWorks
{
    /// <summary>
    /// Specification for repositories, when getting 
    /// when getting entities and etc.
    /// </summary>
    public interface ISpecification<TEntity>
        where TEntity : Entity
    {
        /// <summary>
        /// Includes of sub children of base entity to get.
        /// </summary>
        IReadOnlyCollection<Expression<Func<TEntity, object>>> Includes { get; }

        /// <summary>
        /// Includes of sub children of base entity to get.
        /// </summary>
        IReadOnlyCollection<string> IncludeStrings { get; }

        void Include(string includeString);

        void Include(Expression<Func<TEntity, object>> includeExpression);
    }
}
