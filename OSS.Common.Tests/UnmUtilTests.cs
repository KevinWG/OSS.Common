using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.Common.ComUtils;

namespace OSS.Common.Tests
{
    [TestClass]
    public class UnmUtilTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var snowStr1 = NumUtil.SnowNum();
            var snowStr2 =  NumUtil.SnowNum();
            Assert.IsTrue(snowStr2 != snowStr1);

            var timeNum1 = NumUtil.TimeMilliNum();
            var timeNum2 = NumUtil.TimeMilliNum()+1;
            Assert.IsTrue(timeNum1 != timeNum2);

            var subTimeNum = NumUtil.SubTimeNum(timeNum1);
            Assert.IsTrue(timeNum1 % 10000 == subTimeNum % 10000);
        }
    }
}
