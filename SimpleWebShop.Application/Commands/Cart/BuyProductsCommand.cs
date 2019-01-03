using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimpleWebShop.Domain.Entities;
using SimpleWebShop.Domain.UnitOfWorks;

namespace SimpleWebShop.Application.Commands.Cart
{
    public class BuyProductsCommand : IRequest<bool>
    {
        public BuyProductsCommand(List<InventoryProduct> inventoryProducts)
        {            
            this.InventoryProducts = inventoryProducts;
        }

        public List<InventoryProduct> InventoryProducts { get; set; }
    }

    public class BuyProductsCommandHandler : IRequestHandler<BuyProductsCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public BuyProductsCommandHandler(IUnitOfWork unitofwork)
        {
            if(unitofwork == null)
                throw new ArgumentNullException(nameof(unitofwork));

            this._unitOfWork = unitofwork;
        }
        public async Task<bool> Handle(BuyProductsCommand request, CancellationToken cancellationToken)
        {
            if(request.InventoryProducts == null)
                throw new ArgumentNullException(nameof(request.InventoryProducts));
            if(!request.InventoryProducts.Any())
                throw new ArgumentOutOfRangeException(nameof(request));

            try
            {
                foreach (var inventoryProduct in request.InventoryProducts)
                {
                    var result = this._unitOfWork.Repository.Update(inventoryProduct);
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
