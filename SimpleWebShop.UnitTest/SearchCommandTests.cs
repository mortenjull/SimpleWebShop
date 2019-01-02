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

namespace SimpleWebShop.UnitTest
{
    public class SearchCommandTests
    {

        [Fact]
        public void TestCase1_Fact_True()
        {
            Assert.True(true);
        }

        [Theory]
        [ClassData(typeof(TestSearchDataGenerator))]
        public void TestCase2_Theory_True(double minPrice, double maxPrice, List<int> colors)
        {
            var searchProduct = new SearchProductCommand(minPrice, maxPrice, colors);
            CancellationToken token = new CancellationToken();

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            Mock<IRepository> mockRepository = new Mock<IRepository>();

            mockRepository.Setup(x => x.All<Color>(token)).Returns(new FakeSearchCommandRepository().GetAll);
            mockUnitOfWork.Setup(x => x.Repository).Returns(mockRepository.Object);

            var searchProductHandler = new SearchProductCommandHandler(mockUnitOfWork.Object);

            var list = searchProductHandler.Handle(searchProduct, token).Result;

            Assert.NotEmpty(list);

            Assert.True(false);
        }
    }
}
