using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.Common.Authrization;
using OSS.Common.Encrypt;
using OSS.Common.Extention;
using OSS.Common.Resp;

namespace OSS.Common.Tests
{
    [TestClass]
    public class EncryptTests
    {
        [TestMethod]
        public void TestMethod1()
        {
        }


        [TestMethod]
        public  void SysAuth()
        {
            var key = Guid.NewGuid().ToString().Replace("-", string.Empty);

            var appInfo = new AppAuthorizeInfo
            {
                DeviceId = "mmnsnd",
                IpAddress = "127.0.0.1"
            };

            var newSignData = appInfo.ToTicket("app_hlgu80900p0","1.0", key,"ext");
            appInfo.FromTicket(newSignData);

            Assert.IsTrue(appInfo.CheckSign(key,60, "ext").IsSuccess());
        }

        [TestMethod]
        public void EncryptTest()
        {
            var timeUtc = DateTime.Now.ToUtcMilliSeconds();
            var result = Md5.EncryptHexString("1389085779854n35a5fdhawz56y24pjn3u9d5zp9r1nhpebrxyyu359cq0ddo");
            //var page = new PageListModel<AppAuthorizeInfo>(100, new List<AppAuthorizeInfo>(), new
            //        SearchModel() {});
        }


        [TestMethod]
        public void Base64Tests()
        {
            string str1 =  "m".ToSafeUrlBase64();

            string str2 = str1.FromSafeUrlBase64();
            Assert.IsTrue(str2=="m");
        }


        [TestMethod]
        public void Sha1Test()
        {
            string str1 = Sha1.Encrypt("这是一个测试");
        }

        [TestMethod]
        public void Encrypt()
        {
            string key = Guid.NewGuid().ToString().Replace("-", string.Empty);

            string result = AesRijndael.Encrypt("owxehuN4Ntt8Gx0AqBJ6O6Jv27Yg", key);

            string r = AesRijndael.Decrypt(result, key);
        }

    }


}
