using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.Common.Plugs.LogPlug;

namespace OSS.Common.Tests
{
    [TestClass]
    public class LogTests
    {
        [TestMethod]
        public void TestMethod1()
        {
          var logCode= LogUtil.Error("test", "testkey", "test");
        }
        
    }


}
