using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Moq;
using SimpleWebShop.Application.Commands.Cart;
using SimpleWebShop.Domain.Entities;
using SimpleWebShop.Domain.UnitOfWorks;
using SimpleWebShop.Domain.UnitOfWorks.Repositories;
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

        public void ReturnTrue_AllInventoryProductsUpdated()
        {

        }

        public void ReturnFalse_AnExceptionWasCought()
        {

        }
    }
}
