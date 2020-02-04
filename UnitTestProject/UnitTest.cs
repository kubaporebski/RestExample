using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestExample;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod()
        {
            IEnumerable result = BDL.FetchData("e595b260-5874-48e6-d0a1-08d6d44dd662", 2137, "010000000000", 2015, 2018, 1);
            Assert.IsNotNull(result);

            foreach (Item item in result)
            {
                Assert.IsNotNull(item);
                Assert.IsTrue(item.Year > 0);
            }
            
        }
    }
}
