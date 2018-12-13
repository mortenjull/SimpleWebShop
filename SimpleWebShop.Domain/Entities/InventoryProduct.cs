using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleWebShop.Domain.Entities
{
    public class InventoryProduct
    {
        /// <summary>
        /// Amount of the <see cref="Product"/>.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Price of the <see cref="Product"/>.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Id of the <see cref="Product"/>.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// <see cref="Product"/> in inventory.
        /// </summary>
        public Product Product { get; set; }
    }
}
