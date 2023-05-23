using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OSS.Common.Encrypt;

namespace OSS.Common.Tests
{
    using OSS.Common.Resp;

    [TestClass]
    public class RespTests
    {
        [TestMethod]
        public void ListTokenTest()
        {
            var list = new List<TestItemMo>();
            list.Add(new TestItemMo(){id   =1,age   =20});
            list.Add(new TestItemMo() { id = 2, age = 20 });

            var pageList = new TokenPageListResp<TestItemMo>(2,list);
            pageList.AddColumnToken("age",x=>x.age.ToString(),x=> Md5.EncryptHexString( x.age.ToString()));
        }


        [TestMethod]
        public void RespTest()
        {
           var  res = new StrResp().WithResp(SysRespCodes.NetError, $"微信支付接口请求异常");
        }

        [TestMethod]
        public void RespJsonTest()
        {
            var res = new Resp();
            var str = JsonConvert.SerializeObject(res);

            var revertRes = JsonConvert.DeserializeObject<Resp>(str);
        }

    }

}
