using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleWebShop.Domain.Entities
{
    public class Product
        : Entity
    {
        /// <summary>
        /// Id of the <see cref="Color"/> of the <see cref="Product"/>.
        /// </summary>
        public int ColorId { get; set; }

        /// <summary>
        /// <see cref="Color"/> of the <see cref="Product"/>.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Name of the <see cref="Product"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Inventory.
        /// </summary>
        public InventoryProduct Inventory { get; set; }
    }
}
