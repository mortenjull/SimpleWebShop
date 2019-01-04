using MediatR;
using SimpleWebShop.Domain.Entities;
using SimpleWebShop.Domain.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleWebShop.Application.Commands.Search
{
    public class SearchProductAllColorsCommand
        : IRequest<IReadOnlyCollection<Color>>
    { }

    public class SearchProductAllColorsCommandHandler
        : IRequestHandler<SearchProductAllColorsCommand, IReadOnlyCollection<Color>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchProductAllColorsCommandHandler(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));

            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyCollection<Color>> Handle(
            SearchProductAllColorsCommand request, 
            CancellationToken cancellationToken)
        {
            // Get all colors.
            return await _unitOfWork.Repository.All<Color>(cancellationToken);
        }
    }
}
