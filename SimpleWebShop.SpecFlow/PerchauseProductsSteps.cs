using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using SimpleWebShop.Application.Commands.Cart;
using SimpleWebShop.Domain.Entities;
using SimpleWebShop.Domain.UnitOfWorks;
using SimpleWebShop.Domain.UnitOfWorks.Repositories;
using SimpleWebShop.Shared;
using TechTalk.SpecFlow;
using Xunit;

namespace SimpleWebShop.Specflow
{
    [Binding]
    public class PerchauseProductsSteps
    {
        Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
        Mock<IRepository> repositoryMock = new Mock<IRepository>();

        int _itemId;

        private CheckInventoryCommand _checkInventory;
        private CheckInventoryCommandHandler _checkHandler;
        private ResultObject result;

        //Given I have the item with * id
        [Given(@"I have added item (.*) into the Cart")]
        public void GivenIHaveAddedItemIntoTheCart(string p0)
        {
            _itemId = Int32.Parse(p0);
        }

        //I want to have * amount of set item
        [Given(@"I want this amount (.*)")]
        public void GivenIWantThisAmount(string p0)
        {
            var tempList = new List<int>();
            for (int i = 0; i < Int32.Parse(p0); i++)
            {
                tempList.Add(_itemId);
            }

            _checkInventory = new CheckInventoryCommand(tempList);
        }

        [Given(@"The shop have product (.*) in  stock(.*)")]
        public void GivenTheShopHaveProductInStock(string p0, string p1)
        {
            SetupMock(Int32.Parse(p0), Int32.Parse(p1));
        }

        [When(@"i press Perchause")]
        public async void WhenIPressPerchause()
        {
            CancellationToken handleToken = new CancellationToken();

            _checkHandler = new CheckInventoryCommandHandler(unitOfWorkMock.Object);
            result = await _checkHandler.Handle(_checkInventory, handleToken);
        }

        [Then(@"the result should be succes:(.*)\.")]
        public void ThenTheResultShouldBeSucces_(string p0)
        {
            Assert.Equal(Boolean.Parse(p0), result.Succes);
        }
        public void SetupMock(int id, int amount)
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