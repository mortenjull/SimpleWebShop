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
        public double MinPrice { get; set; }

        /// <summary>
        /// Maximum price for search.
        /// </summary>
        public double MaxPrice { get; set; }

        /// <summary>
        /// List of colors id's.
        /// </summary>
        public List<int> Colors { get; set; }

        /// <summary>
        /// Selected sorting type.
        /// </summary>
        public Sorting SortBy { get; set; } = Sorting.Lowest;

        /// <summary>
        /// Enum for sorting types.
        /// </summary>
        public enum Sorting
        {
            Highest = 2,
            Lowest = 1
        }
    }
}
