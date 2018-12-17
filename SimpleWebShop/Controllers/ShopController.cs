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
            // Default price and max price.
            var defaultMinPrice = 0;
            var defaultMaxPrice = 10000;

            // Get all the default colors to show on the page.
            var defaultColors = await _mediator.Send(new SearchProductAllColorsCommand());
            
            // Create command for getting all products.
            var command = new SearchProductCommand(
                 defaultMinPrice,
                 defaultMaxPrice,
                 defaultColors.Select(x => x.Id).ToList());

            // Execute and search for produts.
            var result = await _mediator.Send(command);

            // Prepare view model for display.
            var viewModel = new ShopSearchViewModel()
            {
                DefaultColors = defaultColors.ToList(),
                DefaultMinPrice = defaultMinPrice,
                DefaultMaxPrice = defaultMaxPrice,

                Colors = defaultColors.Select(x => x.Id).ToList(),
                MaxPrice = defaultMaxPrice,
                MinPrice = defaultMinPrice,
                Products = result.Select(x => new ShopSearchProductViewModel()
                {
                    Name = x.Name,
                    Price = x.Inventory.Price,
                    Picture = x.Picture
                })
            };

            return View("Index", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Search(ShopSearchModel model)
        {
            // Default price and max price.
            var defaultMinPrice = 0;
            var defaultMaxPrice = 10000;

            // Get all the default colors to show on the page.
            var defaultColors = await _mediator.Send(new SearchProductAllColorsCommand());

            var command = new SearchProductCommand(
                model.MinPrice,
                model.MaxPrice,
                model.Colors);

            var result = await _mediator.Send(command);

            var viewModel = new ShopSearchViewModel()
            {
                DefaultColors = defaultColors.ToList(),
                DefaultMinPrice = defaultMinPrice,
                DefaultMaxPrice = defaultMaxPrice,

                Colors = model.Colors,
                MaxPrice = model.MaxPrice,
                MinPrice = model.MinPrice,
                Products = result.Select(x => new ShopSearchProductViewModel()
                {
                    Name = x.Name,
                    Price = x.Inventory.Price,
                    Picture = x.Picture
                })
            };

            return View("Index", viewModel);
        }
    }
}