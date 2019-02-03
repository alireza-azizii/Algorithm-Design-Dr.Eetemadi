using Microsoft.VisualStudio.TestTools.UnitTesting;
using E2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2.Tests
{
    [TestClass()]
    public class Q4TreeDiameterTests//Grade:E2.4:25
    {
        [TestMethod()]
        public void Q4TreeDiameterTest()
        {
            Q4TreeDiameter td = new Q4TreeDiameter(10, 0);
        }

        [TestMethod()]
        public void TreeHeightTest()
        {
            Q4TreeDiameter q4 = new Q4TreeDiameter(5);
            Assert.AreEqual(2, q4.TreeHeight());

        }

        [TestMethod()]
        public void TreeHeightFromNodeTest()
        {
            Q4TreeDiameter q4 = new Q4TreeDiameter(5);
            Assert.AreEqual(4, q4.TreeHeightFromNode(1));
        }

        [TestMethod()]
        public void TreeDiameterN2Test()
        {
            Q4TreeDiameter q4 = new Q4TreeDiameter(5);
            Assert.AreEqual(4, q4.TreeDiameterN2());
        }

        [TestMethod()]
        public void TreeDiameterNTest()
        {
        }
    }
}