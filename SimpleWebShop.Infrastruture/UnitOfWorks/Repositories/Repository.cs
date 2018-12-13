using Microsoft.EntityFrameworkCore;
using SimpleWebShop.Domain.Entities;
using SimpleWebShop.Domain.UnitOfWorks;
using SimpleWebShop.Domain.UnitOfWorks.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleWebShop.Infrastruture.UnitOfWorks.Repositories
{
    public class Repository
        : IRepository
    {
        /// <summary>
        /// EntiyFramework Core db context to use for
        /// repository.
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        /// Constructs a repository which is using entity framework
        /// core to store entities.
        /// </summary>
        /// <param name="dbContext"></param>
        public Repository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            this._dbContext = dbContext;
        }

        public TEntity Add<TEntity>(TEntity entity) where TEntity : Entity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var set = this._dbContext.Set<TEntity>();

            return set.Add(entity).Entity;
        }

        public async Task<IReadOnlyCollection<TEntity>> All<TEntity>(
            CancellationToken cancellationToken) where TEntity : Entity
        {
            if (cancellationToken == null)
                throw new ArgumentNullException(nameof(cancellationToken));

            var set = this._dbContext.Set<TEntity>();

            return await set.ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<TEntity>> All<TEntity>(
            ISpecification<TEntity> specification,
            CancellationToken cancellationToken)
            where TEntity : Entity
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));
            if (cancellationToken == null)
                throw new ArgumentNullException(nameof(cancellationToken));

            var query = ConstructQueryFromSpecification(specification);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<TEntity> FirstOrDefault<TEntity>(
            Expression<Func<TEntity, bool>> criteria,
            CancellationToken cancellationToken)
            where TEntity : Entity
        {
            if (criteria == null)
                throw new ArgumentNullException(nameof(criteria));
            if (cancellationToken == null)
                throw new ArgumentNullException(nameof(cancellationToken));

            var set = this._dbContext.Set<TEntity>();

            return await set.FirstOrDefaultAsync(criteria, cancellationToken);
        }

        public async Task<TEntity> FirstOrDefault<TEntity>(
            IExpSpecification<TEntity> specification,
            CancellationToken cancellationToken)
            where TEntity : Entity
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));
            if (cancellationToken == null)
                throw new ArgumentNullException(nameof(cancellationToken));
            if (specification.Criteria == null)
                throw new ArgumentNullException(nameof(specification));

            var query = ConstructQueryFromSpecification(specification);

            return await query.FirstOrDefaultAsync(specification.Criteria, cancellationToken);
        }

        public TEntity Remove<TEntity>(TEntity entity) where TEntity : Entity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var set = this._dbContext.Set<TEntity>();

            return set.Remove(entity).Entity;
        }

        public async Task<TEntity> Remove<TEntity>(
            Expression<Func<TEntity, bool>> criteria,
            CancellationToken cancellationToken)
            where TEntity : Entity
        {
            if (criteria == null)
                throw new ArgumentNullException(nameof(criteria));

            var entity = await this.FirstOrDefault(criteria, cancellationToken);

            if (entity != null)
                return this._dbContext.Set<TEntity>().Remove(entity).Entity;

            return null;
        }

        public TEntity Update<TEntity>(TEntity entity) where TEntity : Entity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var set = this._dbContext.Set<TEntity>();

            return set.Update(entity).Entity;
        }

        public async Task<IReadOnlyCollection<TEntity>> Where<TEntity>(
            Expression<Func<TEntity, bool>> criteria,
            CancellationToken cancellationToken)
            where TEntity : Entity
        {
            if (criteria == null)
                throw new ArgumentNullException(nameof(criteria));
            if (cancellationToken == null)
                throw new ArgumentNullException(nameof(cancellationToken));

            var set = this._dbContext.Set<TEntity>();

            return await set.Where(criteria).ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<TEntity>> Where<TEntity>(
            IExpSpecification<TEntity> specification,
            CancellationToken cancellationToken)
            where TEntity : Entity
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));
            if (cancellationToken == null)
                throw new ArgumentNullException(nameof(cancellationToken));
            if (specification.Criteria == null)
                throw new ArgumentNullException(nameof(specification));

            var query = ConstructQueryFromSpecification(specification);

            return await query.Where(specification.Criteria).ToListAsync(cancellationToken);
        }

        protected IQueryable<TEntity> ConstructQueryFromSpecification<TEntity>(
            ISpecification<TEntity> specification) where TEntity : Entity
        {
            var set = this._dbContext.Set<TEntity>();

            // Fetch a Queryable that includes all expression-based includes.
            var queryableResultWithIncludes = specification.Includes
                .Aggregate(set.AsQueryable(),
                    (current, include) => current.Include(include));

            // Modify the IQueryable to include any string-based include 
            // statements.
            var secondaryResult = specification.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            return secondaryResult;
        }
    }
}
