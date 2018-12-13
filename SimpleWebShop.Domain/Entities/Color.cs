using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleWebShop.Domain.Entities
{
    public class Color
        : Entity
    {
        /// <summary>
        /// Name of the <see cref="Color"/>.
        /// </summary>
        public string Name { get; set; }
    }
}
