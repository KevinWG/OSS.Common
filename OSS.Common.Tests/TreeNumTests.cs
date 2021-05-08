using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.Common.Helpers;

namespace OSS.Common.Tests
{
    [TestClass]
    public class TreeNumTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var num = TreeNumHelper.GenerateSmallNum(0, 0);
            Assert.IsTrue(num==1000000000000000);
            
            
            num = TreeNumHelper.GenerateSmallNum(1999000000000000, 0);
            Assert.IsTrue(num == 1999010000000000);

            num = TreeNumHelper.GenerateSmallNum(1999000000000000, 1999099900000000);
            Assert.IsTrue(num == 1999011110000000);

            var  pNums = TreeNumHelper.FormatParents(1010100000000000);
            Assert.IsTrue(pNums.Length==2&&pNums[0]== 1000000000000000);

            var firstRange = TreeNumHelper.FormatSubNumRange(1000000000000000);
            Assert.IsTrue(firstRange.maxSubNum == 1099999999999999&& firstRange.minSubNum == 1010000000000000);

            
            var lastRange = TreeNumHelper.FormatSubNumRange(1099999999999900);
            Assert.IsTrue(lastRange.maxSubNum == 1099999999999909 && lastRange.minSubNum == 1099999999999901);
        }

        [TestMethod]
        public void FormatParentsTest()
        {
            var firParents = TreeNumHelper.FormatParents(1000000000000000);
            Assert.IsTrue(firParents[0]==0);

            var parents = TreeNumHelper.FormatParents(1010101011100000);
            Assert.IsTrue(parents.Length == 4);
        }
    }


}
