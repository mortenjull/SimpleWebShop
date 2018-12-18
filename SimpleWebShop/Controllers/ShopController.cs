using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleWebShop.Application.Commands.Cart;
using SimpleWebShop.Application.Commands.Search;
using SimpleWebShop.Domain.Entities;
using SimpleWebShop.Models.Shop;

namespace SimpleWebShop.Controllers
{
    public class ShopController : Controller
    {
        /// <summary>
        /// <see cref="IMediator"/> to use.
        /// </summary>
        private readonly IMediator _mediator;

        public ShopController(IMediator mediator)
        {
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));

            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var defaultMinPrice = 0;
            var defaultMaxPrice = 10000;

            var command = new SearchProductCommand(
                 defaultMinPrice,
                 defaultMaxPrice,
                 null);

            var result = await _mediator.Send(command);

            var viewModel = new ShopSearchViewModel()
            {
                Colors = null,
                MaxPrice = defaultMaxPrice,
                MinPice = defaultMinPrice,
                Products = result.Select(x => new ShopSearchProductViewModel()
                {
                    Name = x.Name,
                    Price = x.Inventory.Price,
                    Picture = x.Picture
                })
            };

            return View("Index", viewModel);
        }

        public async Task<IActionResult> Search(ShopSearchModel model)
        {
            var command = new SearchProductCommand(
                model.MinPice,
                model.MaxPrice,
                model.Colors);

            var result = await _mediator.Send(command);

            var viewModel = new ShopSearchViewModel()
            {
                Colors = model.Colors,
                MaxPrice = model.MaxPrice,
                MinPice = model.MinPice,
                Products = result.Select(x => new ShopSearchProductViewModel()
                {
                    Name = x.Name,
                    Price = x.Inventory.Price,
                    Picture = x.Picture
                })
            };

            return View("Index", viewModel);
        }

        public async Task<IActionResult> BuyProducts(List<int> productIds)
        {
            if(!productIds.Any())
                return new BadRequestObjectResult("You must chose some items to perchause");

            var checkInventoryCommand = new CheckInventoryCommand(productIds);
            var inventoryResult = this._mediator.Send(checkInventoryCommand).Result;

            if(inventoryResult == null || !inventoryResult.Succes)
                return new BadRequestObjectResult("Some items wher out of stock");

            var buycommand = new BuyProductsCommand((List<InventoryProduct>) inventoryResult.Payload);
            var perchauseResult = this._mediator.Send(buycommand).Result;

            if (!perchauseResult)
                return new BadRequestObjectResult("Something went wrong");

            return new OkObjectResult("Yai");
        }
    }
}