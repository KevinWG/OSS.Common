using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OSS.Common.Extension;

namespace OSS.Common.Tests.Extension
{
    [TestClass]
    public class IndentExtensionTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var flatList = new List<TestFlat>();
            
            flatList.Add(new TestFlat() { code = "1", name = "测试1", parnet_code = "0" });
            flatList.Add(new TestFlat() { code = "2", name = "测试2", parnet_code = "0" });
            flatList.Add(new TestFlat() { code = "3", name = "测试3", parnet_code = "0" });
            
            flatList.Add(new TestFlat() { code = "4", name = "测试4", parnet_code = "0" });
            flatList.Add(new TestFlat() { code = "11", name = "测试11", parnet_code = "1" });
            flatList.Add(new TestFlat() { code = "12", name = "测试12", parnet_code = "1" });
            
            flatList.Add(new TestFlat() { code = "13", name = "测试13", parnet_code = "1" });
            flatList.Add(new TestFlat() { code = "21", name = "测试21", parnet_code = "2" });
            flatList.Add(new TestFlat() { code = "22", name = "测试22", parnet_code = "2" });


            var indentlist = flatList.ToIndent<TestFlat,TestIndent>((tf, childrenList) =>
            {
                var ti = new TestIndent();
                ti.name = tf.name;
                ti.code = tf.code;
                ti.children = childrenList;
                return ti;
            }, tf => tf.parnet_code, tf => tf.code, "0");
            Assert.IsTrue(indentlist.Count == 4);

            Assert.IsTrue(indentlist[0].children.Count == 3);

            var newFlatList = indentlist.ToFlat((ti) =>
            {
                var tFlat = new TestFlat();
                tFlat.name = ti.name;
                tFlat.code = ti.code;
                return tFlat;
            }, ti => ti.children);
            Assert.IsTrue(newFlatList.Count == 9);
        }

    }


    public class TestFlat
    {

        public string name { get; set; }

        public string code { get; set; }

        public string parnet_code { get; set; }


    }

    public class TestIndent
    {

        public string name { get; set; }

        public string code { get; set; }

        public IList<TestIndent> children { get; set; }
    }



}
