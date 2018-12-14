using MediatR;
using SimpleWebShop.Domain.Entities;
using SimpleWebShop.Domain.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleWebShop.Application.Commands.Search
{
    public class SearchProductCommand
        : IRequest<IReadOnlyCollection<Product>>
    {
        public SearchProductCommand(double minPrice, double maxPrice, List<int> colors)
        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            Colors = colors;
        }

        public double MinPrice { get; set; }

        public double MaxPrice { get; set; }

        public List<int> Colors { get; set; }
    }

    public class SearchProductCommandHandler
        : IRequestHandler<SearchProductCommand, IReadOnlyCollection<Product>>
    {
        /// <summary>
        /// <see cref="IUnitOfWork"/> to use.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        public SearchProductCommandHandler(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));

            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyCollection<Product>> Handle(
            SearchProductCommand request, 
            CancellationToken cancellationToken)
        {
            // Colors to sort with.
            List<int> colors = request.Colors;

            // If no colors are selected get all colors and use them in search.
            if (colors == null || colors.Any())
                colors = (await _unitOfWork.Repository.All<Color>(cancellationToken)).Select(x => x.Id).ToList();

            // Create filter expression for finding products which is
            // valid under the given criteria.
            var expressionSpecification = new ExpSpecification<Product>(x =>
                x.Inventory.Price > request.MinPrice &&
                x.Inventory.Price < request.MaxPrice &&
                colors.Contains(x.ColorId));

            // What to include.
            expressionSpecification.Include(x => x.Inventory);

            // Execute search for products.
            var result = await _unitOfWork.Repository
                .Where<Product>(expressionSpecification, cancellationToken);

            return result;
        }
    }
}
