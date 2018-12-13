using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleWebShop.Domain.UnitOfWorks
{
    public class ExpSpecification<TEntity>
        : Specification<TEntity>, IExpSpecification<TEntity> where TEntity : IEntity
    {
        public ExpSpecification(Expression<Func<TEntity, bool>> criteria)
            : base()
        {
            if (criteria == null)
                throw new ArgumentNullException(nameof(criteria));

            Criteria = criteria;
        }

        public Expression<Func<TEntity, bool>> Criteria { get; }
    }
}
