using SimpleWebShop.Domain.UnitOfWorks.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleWebShop.Domain.UnitOfWorks
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Repository for saving entities.
        /// </summary>
        IRepository Repository { get; }

        /// <summary>
        /// Saves all the changes which have been made in the
        /// unit of work.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Number of items which got changed on save.</returns>
        Task<int> SaveChanges(CancellationToken cancellationToken = default(CancellationToken));
    }
}
