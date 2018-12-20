using System;
using TechTalk.SpecFlow;
using Xunit;

namespace SimpleWebShop.Specflow
{
    [Binding]
    public class NumberInRangeSteps
    {
        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int p0)
        {
            
        }
        
        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            
        }
        
        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int p0)
        {
            Assert.Equal(120,p0);
        }
    }
}
