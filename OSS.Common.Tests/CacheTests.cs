using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.Common.Plugs.CachePlug;

namespace OSS.Common.Tests
{
    [TestClass]
    public class CacheTests
    {
        [TestMethod]
        public void XmlSerializeTest()
        {
            var id = CacheUtil.Get<long>("sssss");
        }



    }

}
