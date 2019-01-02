using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SimpleWebShop.Domain.Entities;
using SimpleWebShop.Domain.UnitOfWorks;
using SimpleWebShop.Domain.UnitOfWorks.Repositories;

namespace SimpleWebShop.UnitTest
{
    public class FakeInventoryProductRepository : IRepository
    {
        
        public TEntity Add<TEntity>(TEntity entity) where TEntity : Entity
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<TEntity>> All<TEntity>(CancellationToken cancellationToken) where TEntity : Entity
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<TEntity>> All<TEntity>(ISpecification<TEntity> specification, CancellationToken cancellationToken) where TEntity : Entity
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FirstOrDefault<TEntity>(IExpSpecification<TEntity> specification, CancellationToken cancellationToken) where TEntity : Entity
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken) where TEntity : Entity
        {
            throw new NotImplementedException();
        }

        public TEntity Remove<TEntity>(TEntity entity) where TEntity : Entity
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Remove<TEntity>(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken) where TEntity : Entity
        {
            throw new NotImplementedException();
        }

        public TEntity Update<TEntity>(TEntity entity) where TEntity : Entity
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<TEntity>> Where<TEntity>(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken) where TEntity : Entity
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<TEntity>> Where<TEntity>(IExpSpecification<TEntity> specification, CancellationToken cancellationToken) where TEntity : Entity
        {
            throw new NotImplementedException();
        }
    }
}
