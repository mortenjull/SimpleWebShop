using SimpleWebShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleWebShop.Domain.UnitOfWorks.Repositories
{
    public interface IRepository
    {
        /// <summary>
        /// Adds the given entity to the repository.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="entity">Entity to add to repository.</param>
        /// <returns>Entity which gets added to repository.</returns>
        TEntity Add<TEntity>(TEntity entity) where TEntity : Entity;

        /// <summary>
        /// Gets all entities from the repository.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of all entities in repository.</returns>
        Task<IReadOnlyCollection<TEntity>> All<TEntity>(
            CancellationToken cancellationToken)
            where TEntity : Entity;

        /// <summary>
        /// Gets all entities from the repository.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="specification">Specification.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of all entities in repository.</returns>
        Task<IReadOnlyCollection<TEntity>> All<TEntity>(
            ISpecification<TEntity> specification,
            CancellationToken cancellationToken)
            where TEntity : Entity;

        /// <summary>
        /// Finds the first entity in the repository which match
        /// the given expression.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="specification">Specification for entity.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Null if no entity match the criteria, and returns the entity if match.</returns>
        Task<TEntity> FirstOrDefault<TEntity>(IExpSpecification<TEntity> specification, CancellationToken cancellationToken)
            where TEntity : Entity;

        /// <summary>
        /// Finds the first entity in the repository which match
        /// the given expression.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="criteria">Criteria for entity.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Null if no entity match the criteria, and returns the entity if match.</returns>
        Task<TEntity> FirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken)
            where TEntity : Entity;

        /// <summary>
        /// Removes the given entity from the repository.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="entity">Entity to remove.</param>
        /// <returns>Entity which gets removed.</returns>
        TEntity Remove<TEntity>(TEntity entity) where TEntity : Entity;

        /// <summary>
        /// Removes an entitet out from the given expression.
        /// </summary>
        /// <typeparam name="TEntity">Type of entitet.</typeparam>
        /// <param name="criteria">Criteria for finding entitet to remove.</param>
        /// <returns>Removed entity.</returns>
        Task<TEntity> Remove<TEntity>(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken) where TEntity : Entity;

        /// <summary>
        /// Updates the given entity in the repository.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="entity">Entity to update in repository.</param>
        /// <returns>Entity which gets updated.</returns>
        TEntity Update<TEntity>(TEntity entity) where TEntity : Entity;

        /// <summary>
        /// Gets all entities in the repository which match the given
        /// criteria.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="criteria">Criteria for entities.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of all the entities which match the given criteria.</returns>
        Task<IReadOnlyCollection<TEntity>> Where<TEntity>(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken)
            where TEntity : Entity;

        /// <summary>
        /// Gets all entities in the repository which match the given
        /// criteria.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="specification">Specification for entities.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of all the entities which match the given criteria.</returns>
        Task<IReadOnlyCollection<TEntity>> Where<TEntity>(IExpSpecification<TEntity> specification, CancellationToken cancellationToken)
            where TEntity : Entity;
    }
}
