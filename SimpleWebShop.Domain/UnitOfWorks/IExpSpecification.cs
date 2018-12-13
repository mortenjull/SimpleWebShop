using SimpleWebShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SimpleWebShop.Domain.UnitOfWorks
{
    public interface IExpSpecification<TEntity>
        : ISpecification<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Criteria for the specification.
        /// </summary>
        Expression<Func<TEntity, bool>> Criteria { get; }
    }
}
