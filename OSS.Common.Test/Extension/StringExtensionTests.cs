using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.Common.Extension;

namespace OSS.Common.Tests.Extension
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            string testData = "http:// test=str+";

            var esStr     = testData.SafeEscapeUriString();
            var esDataStr = testData.SafeEscapeUriDataString();

            var uStr = esStr.SafeUnescapeUriString();
            var uDataStr = esDataStr.SafeUnescapeUriString();
        }

    }
}
