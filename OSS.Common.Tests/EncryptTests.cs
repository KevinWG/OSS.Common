using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.Common.Authrization;
using OSS.Common.Encrypt;
using OSS.Common.Extention;
using OSS.Common.Plugs.LogPlug;

namespace OSS.Common.Tests
{
    [TestClass]
    public class EncryptTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            SysAuth();
        }


        [TestMethod]
        public void DirConfigTest()
        {
            LogUtil.Info("s");
        }

        public static void SysAuth()
        {
            string key = "8d567449b8714a046c464059788d5fa6";// Guid.NewGuid().ToString().Replace(" - ", string.Empty);
            SysAuthorizeInfo appInfo = new SysAuthorizeInfo();
            appInfo.FromSignData("app_source=Test;app_version=1.0;tenant_id=1646;timespan=1507864154;token=mnbvc-Fd2DqZZrjETS6thQ%3D%3D;sign=air2y62B6hy9pPVym%2BKW5fu6vS4%3D");
            var result = appInfo.CheckSign(key);

            var newSignData = appInfo.ToSignData(key);
            appInfo.FromSignData(newSignData);
            result = appInfo.CheckSign(key);
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
        public void Test()
        {
            string str = LogUtil.Info("test");
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
