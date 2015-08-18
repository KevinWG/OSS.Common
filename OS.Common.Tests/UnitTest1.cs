using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OS.Common.Authrization;
using OS.Common.ComModels;
using OS.Common.Encrypt;
using OS.Common.Extention;

namespace OS.Common.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            string key = "ed9d9afe87144d7a982bee451d3b9107";
            SysAuthorizeInfo appInfo = new SysAuthorizeInfo();
            appInfo.FromSignData("timespan=1434372013;appsource=1;sign=p0VEVhcxzOv6u9VN0TDl12qbH_o=;appclient=1;token=SeBCjJYRkujxEsgv9XupyHY7aMkdQuQcqGMu0wQMbhw=;appversion=1.0;",';');

            var mmm = appInfo.CheckSign(key,'&');

            var newSignData = appInfo.ToSignData(key);
            appInfo.FromSignData(newSignData);
            var result = appInfo.CheckSign(key);
        }

        [TestMethod]
        public void DirConfigTest()
        {
            var teestsets =
                "timespan=1434424329;appsource=1;sign=5vJY-VLYSVGjvEoOhTIXXPD4Gx4=;appclient=1;token=zRgdcEFdeTvIdEwz0GtWP_SJ-DxszWqdqluqF0Rmsow=;appversion=1.0;"
                    .UrlEncode();
            //var
            //    page = new PageListModel<AppAuthorizeInfo>(100, new List<AppAuthorizeInfo>(), new
            //        SearchModel() {});
        }


        [TestMethod]
        public void EncryptTest()
        {
            var timeUtc = DateTime.Now.ToUtcMilliSeconds();
            var result = Md5.EncryptHexString("1389085779854n35a5fdhawz56y24pjn3u9d5zp9r1nhpebrxyyu359cq0ddo");
            //var
            //    page = new PageListModel<AppAuthorizeInfo>(100, new List<AppAuthorizeInfo>(), new
            //        SearchModel() {});
        }


        [TestMethod]
        public void Mmmmmmmm()
        {
            string re = "&".UrlEncode();

            string unRe = "p0VEVhcxzOv6u9VN0TD/l+12qbH_o=".UrlDecode();
        }
      
    }
}
