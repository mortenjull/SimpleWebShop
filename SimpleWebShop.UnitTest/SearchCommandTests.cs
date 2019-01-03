using SimpleWebShop.Application.Commands.Search;
using SimpleWebShop.UnitTest.DataGenerators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Moq;
using Xunit;
using SimpleWebShop.Domain.UnitOfWorks;
using SimpleWebShop.Domain.UnitOfWorks.Repositories;
using SimpleWebShop.Domain.Entities;
using SimpleWebShop.UnitTest.Fake;
using SimpleWebShop.Infrastruture.EFCore;
using Microsoft.EntityFrameworkCore;
using SimpleWebShop.Infrastruture.UnitOfWorks;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebShop.UnitTest
{
    public class SearchCommandTests
    {

        [Theory]
        [ClassData(typeof(TestSearchDataGeneratorValidSearch))]
        public async Task SearchProductComandHandler_ContainsProduct_RightProduct(double minPrice, double maxPrice, List<int> colors)
        {           
            var option = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "SearchProductComandHandler_ContainsProduct_RightProduct")
            .Options;

            var fakeRepository = new FakeSearchCommandRepository();

            List<Color> repoColor = new List<Color>();

            using (var contex = new ApplicationDbContext(option))
            {
                var unitOfwork = new UnitOfWork<ApplicationDbContext>(contex);
                CancellationToken token = new CancellationToken();

                foreach (var color in fakeRepository.GetAllColors())
                {
                    unitOfwork.Repository.Add<Color>(color);
                }

                await unitOfwork.SaveChanges();

                repoColor = (await unitOfwork.Repository.All<Color>(token)).ToList();
            }

            using (var contex = new ApplicationDbContext(option))
            {
                var unitOfwork = new UnitOfWork<ApplicationDbContext>(contex);

                foreach (var product in fakeRepository.GetAllProducts(repoColor))
                {
                    unitOfwork.Repository.Add<Product>(product);
                }

                await unitOfwork.SaveChanges();
            }

            using (var contex = new ApplicationDbContext(option))
            {
                var unitOfwork = new UnitOfWork<ApplicationDbContext>(contex);
                CancellationToken token = new CancellationToken();

                var searchProduct = new SearchProductCommand(minPrice, maxPrice, colors);

                var searchProductHandler = new SearchProductCommandHandler(unitOfwork);
                var results = searchProductHandler.Handle(searchProduct, token).Result;

                Assert.NotEmpty(results);

                if (colors.Any())
                {
                    foreach (var result in results)
                    {
                        Assert.Contains(result.ColorId, colors);
                    }
                }               

                foreach (var result in results)
                {
                    Assert.True(result.Inventory.Price <= maxPrice);
                    Assert.True(result.Inventory.Price >= minPrice);
                }
            }           
        }        

        [Theory]
        [ClassData(typeof(TestSearchDataGeneratorBoundryValues))]
        public async Task SearchProductComandHandler_BoundryValues_EmptyList(double minPrice, double maxPrice, List<int> colors)
        {
            var option = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "SearchProductComandHandler_BoundryValues_EmptyList").Options;

            List<Color> repoColor = new List<Color>();

            using (var contex = new ApplicationDbContext(option))
            {
                var unitOfwork = new UnitOfWork<ApplicationDbContext>(contex);
                CancellationToken token = new CancellationToken();

                foreach (var color in new FakeSearchCommandRepository().GetAllColors())
                {
                    unitOfwork.Repository.Add<Color>(color);
                }

                await unitOfwork.SaveChanges();

                repoColor = (await unitOfwork.Repository.All<Color>(token)).ToList();
            }

            using (var contex = new ApplicationDbContext(option))
            {
                var unitOfwork = new UnitOfWork<ApplicationDbContext>(contex);

                foreach (var product in new FakeSearchCommandRepository().GetAllProducts(repoColor))
                {
                    unitOfwork.Repository.Add<Product>(product);
                }

                await unitOfwork.SaveChanges();
            }

            using (var contex = new ApplicationDbContext(option))
            {
                var unitOfwork = new UnitOfWork<ApplicationDbContext>(contex);
                CancellationToken token = new CancellationToken();

                var searchProduct = new SearchProductCommand(minPrice, maxPrice, colors);

                var searchProductHandler = new SearchProductCommandHandler(unitOfwork);
                var results = searchProductHandler.Handle(searchProduct, token).Result;

                Assert.Empty(results);
            }
        }

        [Theory]
        [ClassData(typeof(TestSearchDataGeneratorInvalidColor))]
        public async Task SearchProductComandHandler_InvalidColor_EmptyList(double minPrice, double maxPrice, List<int> colors)
        {
            var option = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "SearchProductComandHandler_InvalidColor_EmptyList").Options;

            List<Color> repoColor = new List<Color>();

            using (var contex = new ApplicationDbContext(option))
            {
                var unitOfwork = new UnitOfWork<ApplicationDbContext>(contex);
                CancellationToken token = new CancellationToken();

                foreach (var color in new FakeSearchCommandRepository().GetAllColors())
                {
                    unitOfwork.Repository.Add<Color>(color);
                }

                await unitOfwork.SaveChanges();

                repoColor = (await unitOfwork.Repository.All<Color>(token)).ToList();
            }

            using (var contex = new ApplicationDbContext(option))
            {
                var unitOfwork = new UnitOfWork<ApplicationDbContext>(contex);

                foreach (var product in new FakeSearchCommandRepository().GetAllProducts(repoColor))
                {
                    unitOfwork.Repository.Add<Product>(product);
                }

                await unitOfwork.SaveChanges();
            }

            using (var contex = new ApplicationDbContext(option))
            {
                var unitOfwork = new UnitOfWork<ApplicationDbContext>(contex);
                CancellationToken token = new CancellationToken();

                var searchProduct = new SearchProductCommand(minPrice, maxPrice, colors);

                var searchProductHandler = new SearchProductCommandHandler(unitOfwork);
                var results = searchProductHandler.Handle(searchProduct, token).Result;

                Assert.Empty(results);
            }
        }
    }
}
