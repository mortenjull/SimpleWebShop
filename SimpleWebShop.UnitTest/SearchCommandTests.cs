using SimpleWebShop.UnitTest.DataGenerators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

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
            Assert.True(true);
            Assert.Equal(0.0, minPrice);
            Assert.Equal(10000.0, maxPrice);
        }
    }
}
