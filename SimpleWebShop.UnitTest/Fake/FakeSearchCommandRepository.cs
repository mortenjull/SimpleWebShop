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

        public async Task<IReadOnlyCollection<Color>> GetAllColors()
        {
            var temp = new List<Color>();

            temp.Add(new Color() { Id = 1 });
            temp.Add(new Color() { Id = 2 });
            temp.Add(new Color() { Id = 3 });


            return temp;
        }

        public async Task<IReadOnlyCollection<Product>> GetAllProducts()
        {
            var temp = new List<Product>();

            temp.Add(new Product());

            return temp;
        }
    }
}
