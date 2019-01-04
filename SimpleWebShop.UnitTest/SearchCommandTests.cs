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
            
            using (var context = new ApplicationDbContext(option))
            {
                // Clear the in memory database.
                context.Database.EnsureDeleted();

                // Create unit of work for populating data.
                IUnitOfWork unitOfWork = new UnitOfWork<ApplicationDbContext>(context);

                // Add color.
                var color = unitOfWork.Repository.Add(new Color() { Id = 1 });

                // Add product.
                var product = unitOfWork.Repository.Add(new Product()
                {
                    ColorId = color.Id,
                    Inventory = new InventoryProduct() { Price = 5000 }
                });

                // Save changes.
                await unitOfWork.SaveChanges();
            }

            using (var contex = new ApplicationDbContext(option))
            {
                // Crete unit of work to use in testing.
                IUnitOfWork unitOfwork = new UnitOfWork<ApplicationDbContext>(contex);

                // Source for cancelation.
                CancellationTokenSource source = new CancellationTokenSource();

                // Command to execute.
                var command = new SearchProductCommand(minPrice, maxPrice, colors);

                // Handler to test.
                var handler = new SearchProductCommandHandler(unitOfwork);

                // Test handler.
                var results = await handler.Handle(command, source.Token);

                // Check if list is empty.
                Assert.NotEmpty(results);

                foreach (var result in results)
                {
                    if (colors.Any())
                        Assert.Contains(result.ColorId, colors);

                    Assert.True(result.Inventory.Price <= maxPrice && result.Inventory.Price >= minPrice);
                }
            }           
        }        

        [Theory]
        [ClassData(typeof(TestSearchDataGeneratorInvalidColor))]
        [ClassData(typeof(TestSearchDataGeneratorBoundryValues))]
        public async Task SearchProductComandHandler_BoundryValues_EmptyList(double minPrice, double maxPrice, List<int> colors)
        {
            var option = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "SearchProductComandHandler_BoundryValues_EmptyList").Options;

            using (var context = new ApplicationDbContext(option))
            {
                // Clear the in memory database.
                context.Database.EnsureDeleted();

                // Create unit of work for populating data.
                IUnitOfWork unitOfWork = new UnitOfWork<ApplicationDbContext>(context);

                // Add color.
                var color = unitOfWork.Repository.Add(new Color() { Id = 1 });

                // Add product.
                var product = unitOfWork.Repository.Add(new Product()
                {
                    ColorId = color.Id,
                    Inventory = new InventoryProduct() { Price = 5000 }
                });

                // Save changes.
                await unitOfWork.SaveChanges();
            }

            using (var contex = new ApplicationDbContext(option))
            {
                // Crete unit of work to use in testing.
                IUnitOfWork unitOfwork = new UnitOfWork<ApplicationDbContext>(contex);

                // Source for cancelation.
                CancellationTokenSource source = new CancellationTokenSource();

                // Command to execute.
                var command = new SearchProductCommand(minPrice, maxPrice, colors);

                // Handler to test.
                var handler = new SearchProductCommandHandler(unitOfwork);

                // Test handler.
                var results = await handler.Handle(command, source.Token);

                Assert.Empty(results);
            }
        }
    }
}
