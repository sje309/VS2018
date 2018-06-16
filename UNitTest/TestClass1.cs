using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNitTest
{
    [TestFixture]
    public class TestClass1
    {
        Program program = new Program();
        //[Test]
        //public void TestMethod()
        //{
        //    // TODO: Add your test code here
        //    Assert.Pass("Your first passing test");
        //}
        [Test]
        public void TestIsValid()
        {
            int opt = 101;
            Assert.AreEqual(program.IsValid(opt),true);
        }
    }
}
