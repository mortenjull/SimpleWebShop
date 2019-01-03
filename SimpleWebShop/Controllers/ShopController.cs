using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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

        /// <summary>
        /// <see cref="IConfiguration"/> to use.
        /// </summary>
        private readonly IConfiguration _configuration;

        public ShopController(IMediator mediator, IConfiguration configuration)
        {
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            _mediator = mediator;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            // Default price and max price.
            var defaultMinPrice = double.Parse(_configuration["Default:MinPrice"]);
            var defaultMaxPrice = double.Parse(_configuration["Default:MaxPrice"]);

            // Get all the default colors to show on the page.
            var defaultColors = await _mediator.Send(new SearchProductAllColorsCommand());
            
            // Create command for getting all products.
            var command = new SearchProductCommand(
                 defaultMinPrice,
                 defaultMaxPrice,
                 defaultColors.Select(x => x.Id).ToList());

            // Execute and search for produts.
            var result = await _mediator.Send(command);

            // Order the result by lowest price.
            result = result.OrderBy(x => x.Inventory.Price).ToList();

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
                    Picture = x.Picture,
                    Color = x.Color
                })
            };

            return View("Index", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Search(ShopSearchModel model)
        {
            // Default price and max price.
            var defaultMinPrice = double.Parse(_configuration["Default:MinPrice"]);
            var defaultMaxPrice = double.Parse(_configuration["Default:MaxPrice"]);

            // Get all the default colors to show on the page.
            var defaultColors = await _mediator.Send(new SearchProductAllColorsCommand());

            // Create command for executing searhcing for products.
            var command = new SearchProductCommand(
                model.MinPrice,
                model.MaxPrice,
                model.Colors);

            // Get the result of Search.
            var result = await _mediator.Send(command);
            
            // Apply sorting out from the selected sorting.
            if (model.SortBy == ShopSearchModel.Sorting.Highest)
                result = result.OrderByDescending(x => x.Inventory.Price).ToList();
            else if (model.SortBy == ShopSearchModel.Sorting.Lowest)
                result = result.OrderBy(x => x.Inventory.Price).ToList();

            // Prepare view model.
            var viewModel = new ShopSearchViewModel()
            {
                DefaultColors = defaultColors.ToList(),
                DefaultMinPrice = defaultMinPrice,
                DefaultMaxPrice = defaultMaxPrice,

                Colors = model.Colors ?? defaultColors.Select(x => x.Id).ToList(),
                MaxPrice = model.MaxPrice,
                MinPrice = model.MinPrice,
                Products = result.Select(x => new ShopSearchProductViewModel()
                {
                    Name = x.Name,
                    Price = x.Inventory.Price,
                    Picture = x.Picture,
                    Color = x.Color
                }),
                SortBy = model.SortBy
            };

            return View("Index", viewModel);
        }
    }
}