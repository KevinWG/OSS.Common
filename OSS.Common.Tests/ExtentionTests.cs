using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.Common.ComUtils;
using OSS.Common.Extention;

namespace OSS.Common.Tests
{
    [TestClass]
    public class ExtentionTests
    {
        [TestMethod]
        public void XmlSerializeTest()
        {
            var obj = new TestXml();

            obj.Name = new XmlDocument().CreateCDataSection("ssssss");
            obj.items = new List<TestItem>() { new TestItem() { house = "beijing", age = 20 } };

            string reslt = obj.SerializeToXml();//"<xml><name><![CDATA[toUser]]></name> </xml>";
            var res = reslt.DeserializeXml<TestXml>();
        }

        [TestMethod]
        public void DateTimeTest()
        {
            var localTime = DateTime.Now;
            long startTicks = new DateTime(1970, 1, 1).Ticks;

            var seconds1 = (localTime.ToUniversalTime().Ticks - startTicks) / 10000000;
            var seconds2 = (long) (localTime.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds;
            var seconds3 = localTime.ToLocalSeconds();

            var date1 = seconds1.FromUtcSeconds();
            var date2 = new DateTime(1970, 1, 1).AddSeconds(seconds2).ToLocalTime();
            var date3 = seconds3.FromLocalSeconds();
        }

        [TestMethod]
        public void StringTest()
        {
            var testNum = long.MaxValue;
            var code = testNum.ToCode();

            var num = code.ToCodeNum();
            Assert.IsTrue(num==testNum);
        }



        [TestMethod]
        public void EncodingTest()
        {
            string testStr = "* ~测试";

            //var res1 =   .UrlEncode();
            //var res2 = Uri.EscapeUriString(testStr);
        }


    }


    [XmlRoot(ElementName = "xml")]
    public class TestXml
    {
        [XmlElement("name")]
      
        public XmlCDataSection Name { get; set; }

        /// <summary>
        /// /
        /// </summary>
        [XmlElement("Items")]
        public List<TestItem> items { get; set; }
    }


    public class TestItem
    {
        public string house { get; set; }

        public int age { get; set; }
    }
}
