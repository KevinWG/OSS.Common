namespace OSS.Common.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var r1 = SingleInstance<NoConParaClass>.Instance;
            Assert.IsTrue(r1!=null);

            //var r2 = SingleInstance<HaveConParaClass>.Instance;
            //Assert.IsTrue(r2 != null);

            var r2 = SingleInstance<HaveConParaClass>.GetInstance(()=>new HaveConParaClass("test"));
            Assert.IsTrue(r2 != null);

            var c2 = typeof(HaveConParaClass).GetConstructor(Type.EmptyTypes);
            Assert.IsTrue(c2==null);
        }
    }

    public class NoConParaClass
    {
    }

    public class HaveConParaClass:NoConParaClass
    {
        public string name { get; }

        public HaveConParaClass(string name)
        {
            this.name = name;
        }
    }
}