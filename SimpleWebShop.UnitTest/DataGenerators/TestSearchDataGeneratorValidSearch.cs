using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SimpleWebShop.UnitTest.DataGenerators
{
    public class TestSearchDataGeneratorValidSearch : IEnumerable<object[]>
    {
  
        private readonly List<object[]> _data = new List<object[]>
        {           
            new object[] { 0, 10000, new List<int>(){} },          
            new object[] { 5000, 5000, new List<int>(){} },
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
