using Microsoft.VisualStudio.TestTools.UnitTesting;
using A3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A3.Tests
{
    [TestClass()]//Grade:A3:100
    public class ProgramTests
    {
        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A3_TestData")]
        public void Graded_FibonacciTest()
        {
            TestCommon.TestTools.RunLocalTest("A3", Program.ProcessFibonacci, "TD1");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A3_TestData")]
        public void Graded_Fibonacci_LastDigitTest()
        {
            TestCommon.TestTools.RunLocalTest("A3", Program.ProcessFibonacci_LastDigit, "TD2");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A3_TestData")]
        public void Graded_GCDTest()
        {
            TestCommon.TestTools.RunLocalTest("A3", Program.ProcessGCD, "TD3");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A3_TestData")]
        public void Graded_LMCTest()
        {
            TestCommon.TestTools.RunLocalTest("A3", Program.ProcessLMC, "TD4");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A3_TestData")]
        public void Graded_FibonacciModTest()
        {
            TestCommon.TestTools.RunLocalTest("A3", Program.ProcessFibonacci_Mod, "TD5");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A3_TestData")]
        public void Graded_FibonacciSumTest()
        {
            TestCommon.TestTools.RunLocalTest("A3", Program.ProcessFibonacci_Sum, "TD6");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A3_TestData")]
        public void Graded_FibonacciPartialSumTest()
        {
            TestCommon.TestTools.RunLocalTest( "A3",Program.ProcessFibonacci_Partial_Sum, "TD7");
        }
        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A3_TestData")]
        public void Graded_FibonacciSumSquaresTest()
        {
            TestCommon.TestTools.RunLocalTest("A3" ,Program.ProcessFibonacci_Sum_Squares, "TD8");
        }
    }
}