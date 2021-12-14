using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.Common.Resp;
using OSS.Common.Encrypt;

namespace OSS.Common.Tests
{
    [TestClass]
    public class RespTests
    {
        [TestMethod]
        public void ListTokenTest()
        {
            var list = new List<TestItemMo>();
            list.Add(new TestItemMo(){id   =1,age   =20});
            list.Add(new TestItemMo() { id = 2, age = 20 });

            var pageList = new PageTokenListResp<TestItemMo>(2,list);
            pageList.AddColumnToken("age",x=>x.age.ToString(),x=> Md5.EncryptHexString( x.age.ToString()));
        }


    

    }

}
