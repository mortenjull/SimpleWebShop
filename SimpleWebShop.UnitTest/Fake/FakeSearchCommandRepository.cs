using SimpleWebShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleWebShop.UnitTest.Fake
{
    public class FakeSearchCommandRepository
    {

        List<Color> colors = new List<Color>();
        List<InventoryProduct> inventories = new List<InventoryProduct>();

        public FakeSearchCommandRepository()
        {
            colors.Add(new Color());

            inventories.Add(new InventoryProduct() { Price = 5000 });

        }


        public IReadOnlyCollection<Color> GetAllColors()
        {
            return colors;
        }

        public IReadOnlyCollection<Product> GetAllProducts(List<Color> repoColors)
        {
            var temp = new List<Product>();

            foreach(var inventory in inventories)
            {
                temp.Add(new Product() { Inventory = inventory , ColorId = repoColors[0].Id });
            }

            return temp;
        }
    }
}
