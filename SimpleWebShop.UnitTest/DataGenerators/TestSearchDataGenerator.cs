using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SimpleWebShop.UnitTest.DataGenerators
{
    public class TestSearchDataGenerator : IEnumerable<object[]>
    {
  
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] { 0.0, 10000.0, new List<int>(){} },
            new object[] { 0.0, 10000.0, new List<int>(){ 1, 2 , 3 } },
            new object[] { 0.0, 10000.0, new List<int>(){ 1 } },
            new object[] { 0.0, 10000.0, new List<int>(){ 1, 2 } },
            new object[] { 0.0, 10000.0, new List<int>(){ 2 } },
            new object[] { 0.0, 10000.0, new List<int>(){ 2 , 3 } },
            new object[] { 0.0, 10000.0, new List<int>(){ 3 } }
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
