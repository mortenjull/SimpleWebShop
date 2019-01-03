using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Moq;
using SimpleWebShop.Application.Commands.Cart;
using SimpleWebShop.Domain.Entities;
using SimpleWebShop.Domain.UnitOfWorks;
using SimpleWebShop.Domain.UnitOfWorks.Repositories;
using SimpleWebShop.Infrastruture.EFCore;
using SimpleWebShop.Infrastruture.UnitOfWorks;
using Xunit;

namespace SimpleWebShop.UnitTest
{
    public class BuyProductsCommandHandlerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfwork;
        public BuyProductsCommandHandlerTest()
        {
            _unitOfwork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public void ThrowArgumentNullException_UnitOfWorkIsNull()
        {
            Assert.ThrowsAny<ArgumentNullException>(() => new BuyProductsCommandHandler(null));
        }

        [Fact]
        public void ThrowArgumentNullException_InventoryProductsIsNull()
        {
            var command = new BuyProductsCommand(null);
            var handler = new BuyProductsCommandHandler(this._unitOfwork.Object);

            Assert.ThrowsAnyAsync<ArgumentNullException>(() => handler.Handle(command, new CancellationToken()));
        }

        [Fact]
        public void ThrowArgumentOutOfRangeException_InventoryProductsIsEmpty()
        {
            var command = new BuyProductsCommand(new List<InventoryProduct>());
            var handler = new BuyProductsCommandHandler(this._unitOfwork.Object);

            Assert.ThrowsAnyAsync<ArgumentOutOfRangeException>(() => handler.Handle(command, new CancellationToken()));
        }

        [Fact]
        public async void ReturnTrue_AllInventoryProductsUpdated()
        {           
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ReturnTrue_AllInventoryProductsUpdated")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {     
                var unitOfWork = new UnitOfWork<ApplicationDbContext>(context);

                unitOfWork.Repository.Add(new InventoryProduct() { Amount = 5, Id = 1, ProductId = 1, Price = 10.0 });
                unitOfWork.Repository.Add(new InventoryProduct() { Amount = 5, Id = 2, ProductId = 2, Price = 10.0 });

                await unitOfWork.SaveChanges();                
            }

            using (var context = new ApplicationDbContext(options))
            {
                var unitOfWork = new UnitOfWork<ApplicationDbContext>(context);

                var inventoryProducts = new List<InventoryProduct>()
                {
                    new InventoryProduct(){Amount = 4, Id = 1, ProductId = 1 , Price = 10.0},
                    new InventoryProduct(){Amount = 4, Id = 2, ProductId = 2, Price = 10.0}
                };

                var command = new BuyProductsCommand(inventoryProducts);

                var handler = new BuyProductsCommandHandler(unitOfWork);

                Assert.True(handler.Handle(command, new CancellationToken()).Result);
            }
        }

        [Fact]
        public async void ReturnFalse_AnExceptionWasCought()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ReturnFalse_AnExceptionWasCought")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var unitOfWork = new UnitOfWork<ApplicationDbContext>(context);

                unitOfWork.Repository.Add(new InventoryProduct() { Amount = 5, Id = 1, ProductId = 1, Price = 10.0 });
                unitOfWork.Repository.Add(new InventoryProduct() { Amount = 5, Id = 2, ProductId = 2, Price = 10.0 });

                await unitOfWork.SaveChanges();

                var inventoryProducts = new List<InventoryProduct>()
                {
                    new InventoryProduct(){Amount = 4, Id = 1, ProductId = 1 , Price = 10.0},
                    new InventoryProduct(){Amount = 4, Id = 2, ProductId = 2, Price = 10.0}
                };

                var command = new BuyProductsCommand(inventoryProducts);

                var handler = new BuyProductsCommandHandler(unitOfWork);

                Assert.ThrowsAnyAsync<Exception>(() => handler.Handle(command, new CancellationToken()));
            }          
        }
    }
}
