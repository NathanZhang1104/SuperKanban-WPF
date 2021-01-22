using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperKanban.Model.Control;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperKanban.Model.Control.Tests
{
    [TestClass()]
    public class MonitorTests
    {
        [TestMethod()]
        public void ExecuteTest()
        {
            Monitor monitor = new Monitor();
            monitor.Execute();
            Assert.AreEqual(1,1);
        }
    }
}