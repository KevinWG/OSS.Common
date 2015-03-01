using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OS.Common.LogModule;

namespace OS.Infrastruct.Tests
{
    [TestClass]
    public class LogHelperTest
    {
        [TestMethod]
        public void WriteInfoLogTest()
        {
            LogUtil.Info("test","testtstetsets");
        }
    }
}
