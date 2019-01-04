using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SimpleWebShop.UnitTest.DataGenerators
{
    public class TestSearchDataGeneratorInvalidColor : IEnumerable<object[]>
    {

        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] { 0, 10000, new List<int>(){ -1 } },
            new object[] { 0, 4999, new List<int>(){ -1 } },
            new object[] { 5001, 10000, new List<int>(){ -1 } },
            new object[] { 5001, 0, new List<int>(){ -1 } },
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
