using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimpleWebShop.Domain.Entities;
using SimpleWebShop.Domain.UnitOfWorks;
using SimpleWebShop.Shared;

namespace SimpleWebShop.Application.Commands.Cart
{
    public class CheckInventoryCommand : IRequest<ResultObject>
    {
        public CheckInventoryCommand(List<int> productIds)
        {
            this.ProductIds = productIds;
        }

        public List<int> ProductIds { get; set; }
    }

    public class CheckInventoryCommandHandler : IRequestHandler<CheckInventoryCommand, ResultObject>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckInventoryCommandHandler(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));
            this._unitOfWork = unitOfWork;
        }
        public async Task<ResultObject> Handle(
            CheckInventoryCommand request, 
            CancellationToken cancellationToken)
        {
            if(request.ProductIds == null)
                throw new ArgumentNullException(nameof(request));
            if (!request.ProductIds.Any())
                throw new ArgumentOutOfRangeException(nameof(request.ProductIds));
            
            var productAndAmount = new Dictionary<int, int>();

            //Combines dublicates
            foreach (var id in request.ProductIds)
            {
                int amount = 1;
                if (!productAndAmount.TryGetValue(id, out amount))
                {
                    productAndAmount.Add(id, amount);
                }
                else
                {
                    productAndAmount.Remove(id);
                    productAndAmount.Add(id, (amount++));
                }
            }

            //Checks if the items are in stock
            var InventoryProductsToUpdate = new List<InventoryProduct>();
            var ProductsOutOfStock = new List<InventoryProduct>();
            foreach (var product in productAndAmount)
            {
                var result =
                    await this._unitOfWork.Repository.
                        FirstOrDefault<InventoryProduct>
                            (i => i.ProductId == product.Key, cancellationToken);

                if (result != null)
                {
                    if (result.Amount >= product.Value)
                    {                        
                        var newamount = result.Amount - product.Value;
                        result.Amount = newamount;
                        InventoryProductsToUpdate.Add(result);
                    }
                    else
                    {
                        ProductsOutOfStock.Add(result);
                    }
                }
                else
                {
                    return null;
                }
            }

            if (ProductsOutOfStock.Any())
            {
                var result = new ResultObject();
                result.Succes = false;
                result.Payload = ProductsOutOfStock;
                return result;
            }
            else
            {
                var result = new ResultObject();
                result.Succes = true;
                result.Payload = InventoryProductsToUpdate;
                return result;
            }                          
        }      
    }
}
