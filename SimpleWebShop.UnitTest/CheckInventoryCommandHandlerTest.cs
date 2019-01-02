using System;
using System.Collections.Generic;
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
            _inventoryProductRepo = new Mock<IRepository>();

            _unitOfwork = new Mock<IUnitOfWork>();
            this._unitOfwork.Setup(mock => mock.Repository).Returns(new Repository(new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>())));

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
            var command = new CheckInventoryCommand(new List<int>(){1,2,3});    

            var handler = new CheckInventoryCommandHandler(this._unitOfwork.Object);

            this._unitOfwork.Setup(mock =>
                mock.Repository.FirstOrDefault<InventoryProduct>(i => i.Id == -1, new CancellationToken()));
                      
            Assert.Null(handler.Handle(command, new CancellationToken()).Result);
        }

        [Fact]
        public void ReturnTrue_AllProductsWasInStock()
        {
            var command = new CheckInventoryCommand(new List<int>() { 1});

            var handler = new CheckInventoryCommandHandler(this._unitOfwork.Object);

            this._unitOfwork.Setup(mock =>
                mock.Repository.FirstOrDefault<InventoryProduct>(x => x.ProductId == It.IsAny<int>(), new CancellationToken()))
                .Returns(Task.FromResult(new InventoryProduct() { Amount = 5, Id = 1, ProductId = 1, Price = 10.0 }));

            


            var result = handler.Handle(command, new CancellationToken()).Result;
            Assert.True(result.Succes);
        }

        [Fact]
        public void ReturnFalse_NotAllProductsWasInStock()
        {
            var command = new CheckInventoryCommand(new List<int>() { 1, 2 });

            var handler = new CheckInventoryCommandHandler(this._unitOfwork.Object);

            this._unitOfwork.Setup(mock =>
                    mock.Repository.FirstOrDefault<InventoryProduct>(x => x.ProductId == It.IsAny<int>(), new CancellationToken()))
                .Returns(Task.FromResult(new InventoryProduct() { Amount = 5, Id = 1, ProductId = 1, Price = 10.0 }));

            this._unitOfwork.Setup(mock =>
                    mock.Repository.FirstOrDefault<InventoryProduct>(x => x.ProductId == It.IsAny<int>(), new CancellationToken()))
                .Returns(Task.FromResult(new InventoryProduct() { Amount = 0, Id = 2, ProductId = 2, Price = 10.0 }));



            var result = handler.Handle(command, new CancellationToken()).Result;
            Assert.False(result.Succes);
        }

        [Fact]
        public void ReturnsFalse_NoProductsWasInStock()
        {
            var command = new CheckInventoryCommand(new List<int>() { 1});

            var handler = new CheckInventoryCommandHandler(this._unitOfwork.Object);

            this._unitOfwork.Setup(mock =>
                    mock.Repository.FirstOrDefault<InventoryProduct>(x => x.ProductId == It.IsAny<int>(), new CancellationToken()))
                .Returns(Task.FromResult(new InventoryProduct() { Amount = 0, Id = 1, ProductId = 1, Price = 10.0 }));           

            var result = handler.Handle(command, new CancellationToken()).Result;
            Assert.False(result.Succes);
        }



    }
}
