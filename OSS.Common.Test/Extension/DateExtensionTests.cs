using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.Common.Extension;

namespace OSS.Common.Tests.Extension
{
    [TestClass]
    public class DateExtensionTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var date  = DateTime.Now.ToMonthEnd();
 
        }

    }
}
