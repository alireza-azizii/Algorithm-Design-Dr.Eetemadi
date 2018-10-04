using Microsoft.VisualStudio.TestTools.UnitTesting;
using A2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace A2.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        
        [TestMethod()]
        [DeploymentItem("TestData", "A2_TestData")]
        public void GradedTest_Correctness()
        {
            TestCommon.TestTools.RunLocalTest("A2",Program.Process);
        }
        [TestMethod(), Timeout(500)]
        [DeploymentItem("TestData", "A2_TestData")]
        public void GradedTest_Performance()
        {
            TestCommon.TestTools.RunLocalTest("A2",Program.Process);
        }

        [TestMethod()]
        public void GradedTest_Stress()
        {
            List<int> input;
            Stopwatch timer = new Stopwatch();
            timer.Start();
            while (true)
            {
                Random rnd = new Random();
                int Length = rnd.Next(2, 10);
                input = new List<int>(Length);
                for (int i = 0; i < input.Capacity; i++)
                {
                    input.Add(rnd.Next(0, 10));
                }
                int actual = Program.FastMaxPairwiseProduct(input);
                int expected = Program.NaiveMaxPairwiseProduct(input);

                Assert.AreEqual(expected, actual);

                if (timer.ElapsedMilliseconds == 5000)
                    break;
            }
        }
        
    }
}