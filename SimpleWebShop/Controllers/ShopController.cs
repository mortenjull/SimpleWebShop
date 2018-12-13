using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleWebShop.Application.Commands.Search;
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
            var defaultColors = new List<int>() { 1 };

            var command = new SearchProductCommand(
                 defaultMinPrice,
                 defaultMaxPrice,
                 defaultColors);

            var result = await _mediator.Send(command);

            var viewModel = new ShopSearchViewModel()
            {
                Colors = defaultColors,
                MaxPrice = defaultMaxPrice,
                MinPice = defaultMinPrice,
                Products = result.Select(x => new ShopSearchProductViewModel()
                {
                    Name = x.Name,
                    Price = x.Inventory.Price
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
                    Price = x.Inventory.Price
                })
            };

            return View("Index", viewModel);
        }
    }
}