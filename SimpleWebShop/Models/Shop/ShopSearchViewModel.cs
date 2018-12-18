using SimpleWebShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebShop.Models.Shop
{
    public class ShopSearchViewModel
        : ShopSearchModel
    {
        /// <summary>
        /// Default min price.
        /// </summary>
        public double DefaultMinPrice { get; set; }

        /// <summary>
        /// Default max price.
        /// </summary>
        public double DefaultMaxPrice { get; set; } 

        /// <summary>
        /// Default <see cref="Color"/>.
        /// </summary>
        public List<Color> DefaultColors { get; set; }

        /// <summary>
        /// List of products.
        /// </summary>
        public IEnumerable<ShopSearchProductViewModel> Products { get; set; }
    }

    public class ShopSearchProductViewModel
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public string Color { get; set; }

        public string Picture { get; set; }
    }
}
