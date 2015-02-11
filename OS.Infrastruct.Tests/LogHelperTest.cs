using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OS.Common.Log;

namespace OS.Infrastruct.Tests
{
    [TestClass]
    public class LogHelperTest
    {
        [TestMethod]
        public void WriteInfoLogTest()
        {
            LogHelper.Info("test","testtstetsets");
        }
    }
}
