using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OS.Common.Authrization;
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

            string key = Guid.NewGuid().ToString().Replace("-",string.Empty);
            SysAuthorizeInfo appInfo = new SysAuthorizeInfo();
            appInfo.FromSignData("timespan=1434372013;appsource=1;appclient=1;token=SeBCjJYRkujxEsgv9XupyHY7aMkdQuQcqGMu0wQMbhw=;appversion=1.0;",';');


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


        [TestMethod]
        public void Encrypt()
        {
            //string publickey = @"<RSAKeyValue><Modulus>MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCzP6cxWDtshimWH/jzieQeoCLZwpO8C6paTPpC4bM3dug7GwLNlFmBFFpjEzP92uenD2qyEHXb1Ei0VXnfHMuLh+AqZiciqx8M+Y0fCRQ2bb01HosgiH0fCf7JXjw+8ZkERIk/N00RriIOc+ek3qUF2UJ+gjNxoORijSfYcLJ2zQID</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            //rsa.FromXmlString(publickey);
            //byte[] cipherbytes = rsa.Encrypt(Encoding.Default.GetBytes("owxehuN4Ntt8Gx0AqBJ6O6Jv27Yg"), false);

            //string result= Convert.ToBase64String(cipherbytes);


            string key = "3fb3afd4a9304519b8f265d4ed059073";

            string result = AesRijndael.Encrypt("owxehuN4Ntt8Gx0AqBJ6O6Jv27Yg", key);

            string r = AesRijndael.Decrypt("kicMdru9cwsGUQoDvhWbFdD6id0ZhK/NY79WWJox7sUx=", key);

        }
    }
}
