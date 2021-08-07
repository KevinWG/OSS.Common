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
            string testData = " test+";

            var esStr     = testData.EscapedUriStringOrDefault();
            var esDataStr = testData.EscapedUriDataStringOrDefault();

            var data = testData.UnescapedUriStringOrDefault();

        }

    }
}
