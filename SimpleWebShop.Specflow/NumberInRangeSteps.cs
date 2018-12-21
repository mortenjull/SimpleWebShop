using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Xunit;

namespace SimpleWebShop.Specflow
{
    [Binding]
    public class NumberInRangeSteps
    {
        List<int> numbers = new List<int>();
        int result = 0; 

        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int p0)
        {
            numbers.Add(p0);
        }
        
        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            foreach(int number in numbers)
            {
                result += number;
            }
        }
        
        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int p0)
        {
            Assert.Equal(result, p0);
        }
    }
}
