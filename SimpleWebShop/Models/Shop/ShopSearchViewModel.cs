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
        /// List of products.
        /// </summary>
        public IEnumerable<ShopSearchProductViewModel> Products { get; set; }
    }

    public class ShopSearchProductViewModel
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public int Color { get; set; }

        public string Picture { get; set; }
    }
}
