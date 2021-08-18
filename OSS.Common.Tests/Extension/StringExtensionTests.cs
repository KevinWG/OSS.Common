using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OSS.Common.Extension;

namespace OSS.Common.Tests.Extension
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            string testData = " test=str+";

            var esStr     = testData.SafeEscapeUriString();
            var esDataStr = testData.SafeEscapeUriDataString();

            var data = testData.SafeUnescapeUriString();
        }

    }
}
