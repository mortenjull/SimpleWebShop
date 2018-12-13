using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebShop.Models.Shop
{
    public class ShopSearchModel
    {
        /// <summary>
        /// Minimum price for search.
        /// </summary>
        public double MinPice { get; set; }

        /// <summary>
        /// Maximum price for search.
        /// </summary>
        public double MaxPrice { get; set; }

        /// <summary>
        /// List of colors id's.
        /// </summary>
        public List<int> Colors { get; set; }
    }
}
