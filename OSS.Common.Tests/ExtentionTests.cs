using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            obj.Name =  new XmlDocument().CreateCDataSection("ssssss") ;
            obj.items = new List<TestItem>() {new TestItem() {house = "beijing", age = 20}};

            string reslt = obj.SerializeToXml();//"<xml><name><![CDATA[toUser]]></name> </xml>";
            var res = reslt.DeserializeXml<TestXml>();
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
