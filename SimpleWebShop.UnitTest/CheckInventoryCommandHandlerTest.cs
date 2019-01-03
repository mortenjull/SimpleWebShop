using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using SimpleWebShop.Application.Commands.Cart;
using SimpleWebShop.Domain.Entities;
using SimpleWebShop.Domain.UnitOfWorks;
using SimpleWebShop.Domain.UnitOfWorks.Repositories;
using SimpleWebShop.Infrastruture.EFCore;
using SimpleWebShop.Infrastruture.UnitOfWorks;
using SimpleWebShop.Infrastruture.UnitOfWorks.Repositories;
using Xunit;

namespace SimpleWebShop.UnitTest
{
    public class CheckInventoryCommandHandlerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfwork;
        private readonly Mock<IRepository> _inventoryProductRepo;

        public CheckInventoryCommandHandlerTest()
        {                      
            _unitOfwork = new Mock<IUnitOfWork>();          
        }
        [Fact]
        public void ThrowArgumentNullException_UnitOfWorkIsNull()
        {
            Assert.ThrowsAny<ArgumentNullException>(() => new CheckInventoryCommandHandler(null));
        }

        [Fact]
        public void ThrowArgumentNullException_ProductIdsNull()
        {
            var command = new CheckInventoryCommand(null);

            var handler = new CheckInventoryCommandHandler(this._unitOfwork.Object);

            Assert.ThrowsAnyAsync<ArgumentNullException>(() => handler.Handle(command, new CancellationToken()));
        }

        [Fact]
        public void ArgumentOutOfRangeException_ProductIdsEmpty()
        {
            var command = new CheckInventoryCommand(new List<int>());

            var handler = new CheckInventoryCommandHandler(this._unitOfwork.Object);

            Assert.ThrowsAnyAsync<ArgumentOutOfRangeException>(() => handler.Handle(command, new CancellationToken()));
        }

        [Fact]
        public void ReturnNull_NullReturFromDB()
        {          
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ReturnNull_NullReturFromDB")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var unitOfWork = new UnitOfWork<ApplicationDbContext>(context);

                var command = new CheckInventoryCommand(new List<int>() { 1, 2, 3 });

                var handler = new CheckInventoryCommandHandler(unitOfWork);
                
                Assert.Null(handler.Handle(command, new CancellationToken()).Result);
            }           
        }

        [Fact]
        public async void ReturnTrue_AllProductsWasInStock()
        {                                 
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ReturnTrue_AllProductsWasInStock")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var unitOfWork = new UnitOfWork<ApplicationDbContext>(context);
                unitOfWork.Repository.Add(new InventoryProduct() {Amount = 5, Id = 1, ProductId = 1, Price = 10.0});

                await unitOfWork.SaveChanges();

                var command = new CheckInventoryCommand(new List<int>() { 1 });

                var handler = new CheckInventoryCommandHandler(unitOfWork);

                var result = handler.Handle(command, new CancellationToken()).Result;
                Assert.True(result.Succes);
            }            
        }

        [Fact]
        public async void ReturnFalse_NotAllProductsWasInStock()
        {          
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ReturnFalse_NotAllProductsWasInStock")
                .Options;


            using (var context = new ApplicationDbContext(options))
            {
                var unitOfWork = new UnitOfWork<ApplicationDbContext>(context);
                unitOfWork.Repository.Add(new InventoryProduct() { Amount = 5, Id = 1, ProductId = 1, Price = 10.0 });
                unitOfWork.Repository.Add(new InventoryProduct() {Amount = 0, Id = 2, ProductId = 2, Price = 10.0});

                await unitOfWork.SaveChanges();

                var command = new CheckInventoryCommand(new List<int>() { 1, 2 });

                var handler = new CheckInventoryCommandHandler(unitOfWork);

                var result = handler.Handle(command, new CancellationToken()).Result;
                Assert.False(result.Succes);
            }            
        }

        [Fact]
        public async void ReturnsFalse_NoProductsWasInStock()
        {          
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ReturnsFalse_NoProductsWasInStock")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var unitOfWork = new UnitOfWork<ApplicationDbContext>(context);
                unitOfWork.Repository.Add(new InventoryProduct() { Amount = 0, Id = 1, ProductId = 1, Price = 10.0 });
                
                await unitOfWork.SaveChanges();

                var command = new CheckInventoryCommand(new List<int>() { 1 });

                var handler = new CheckInventoryCommandHandler(unitOfWork);

                var result = handler.Handle(command, new CancellationToken()).Result;
                Assert.False(result.Succes);
            }
        }



    }
}
