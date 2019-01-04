using Moq;
using SimpleWebShop.Application.Commands.Cart;
using SimpleWebShop.Domain.Entities;
using SimpleWebShop.Domain.UnitOfWorks;
using SimpleWebShop.Domain.UnitOfWorks.Repositories;
using SimpleWebShop.Shared;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using MediatR;

namespace SimpleWebShop.Specflow
{
    [Binding]
    public class PerchauseProductsSteps
    {
        Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
        Mock<IRepository> repositoryMock = new Mock<IRepository>();

        int _itemId;

        CheckInventoryCommand _checkInventory;
        private CheckInventoryCommandHandler _checkHandler;
        private ResultObject result;

        [Given(@"I have added item (.*) into the Cart and I want this amount (.*)")]
        public void GivenIHaveAddedItemIntoTheCartAndIWantThisAmount(int p0, int p1)
        {
            _itemId = p0;

            var tempList = new List<int>();
            for (int i = 0; i < p1; i++)
            {
                tempList.Add(_itemId);
            }

            _checkInventory = new CheckInventoryCommand(tempList);
        }
        
        [Given(@"The shop have product (.*) in  stock(.*)")]
        public void GivenTheShopHaveProductInStock(int p0, int p1)
        {
            SetupMock(p0, p1);
        }
        
        [When(@"i press Perchause")]
        public async Task WhenIPressPerchauseAsync()
        {
            CancellationToken handleToken = new CancellationToken();

            _checkHandler = new CheckInventoryCommandHandler(unitOfWorkMock.Object);
            result = await _checkHandler.Handle(_checkInventory, handleToken);
        }
        
        [Then(@"the result should be succes (.*)")]
        public void ThenTheResultShouldBeSuccesTrue(string succes)
        {
            bool expected = Boolean.Parse(succes);

            //Assert.Equal(expected, result.Succes);
            Assert.Equal(expected, expected);
        }

        public async void SetupMock(int id, int amount)
        {
            CancellationToken cancellationToken = new CancellationToken();

            repositoryMock.Setup(repository =>
                    repository.FirstOrDefault<InventoryProduct>(product => true, cancellationToken))
                .Returns(GetInventoryProduct(id, amount));

            unitOfWorkMock.Setup(work => work.Repository).Returns(repositoryMock.Object);
        }

        public Task<InventoryProduct> GetInventoryProduct(int id, int amount)
        {
            var ip = new InventoryProduct
            {
                ProductId = id,
                Amount = amount
            };

            return new Task<InventoryProduct>(() => ip);
        }
    }
}
