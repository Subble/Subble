using Microsoft.VisualStudio.TestTools.UnitTesting;
using Subble.Core.Plugin;

namespace Subble.Test
{
    [TestClass]
    public class CoreSemVersion
    {
        [TestMethod]
        public void Test_SemVersion_Creation()
        {
            var v1 = new SemVersion();
            var v2 = new SemVersion(1,2,3);

            Assert.AreEqual<uint>(0, v1.Major);
            Assert.AreEqual<uint>(0, v1.Minor);
            Assert.AreEqual<uint>(0, v1.Patch);

            Assert.AreEqual<uint>(1, v2.Major);
            Assert.AreEqual<uint>(2, v2.Minor);
            Assert.AreEqual<uint>(3, v2.Patch);
        }

        [TestMethod]
        public void Test_SemVersion_Comparation()
        {
            var v1 = new SemVersion();
            var v2 = new SemVersion(1, 2, 0);
            var v3 = new SemVersion(0, 2, 100);
            var v4 = new SemVersion(2, 0, 254);
            var v5 = new SemVersion(2, 0, 10);
            var v6 = new SemVersion(0, 2, 100);
            var v7 = new SemVersion(2, 1, 254);

            Assert.IsTrue(v1 < v2, $"{v1} < {v2}");
            Assert.IsTrue(v1 < v3, $"{v1} < {v3}");
            Assert.IsTrue(v1 < v4, $"{v1} < {v4}");
            Assert.IsTrue(v1 < v5, $"{v1} < {v5}");
            Assert.IsTrue(v1 < v6, $"{v1} < {v6}");

            Assert.IsTrue(v2 > v1, $"{v2} > {v1}");
            Assert.IsTrue(v2 > v3, $"{v2} > {v3}");
            Assert.IsTrue(v2 < v4, $"{v2} < {v4}");
            Assert.IsTrue(v2 < v5, $"{v2} < {v5}");
            Assert.IsTrue(v2 > v6, $"{v2} > {v6}");

            Assert.IsTrue(v3 != v1, $"{v3} != {v1}");
            Assert.IsTrue(v3 != v2, $"{v3} != {v2}");
            Assert.IsTrue(v3 != v4, $"{v3} != {v4}");
            Assert.IsTrue(v3 != v5, $"{v3} != {v5}");
            Assert.IsTrue(v3 == v6, $"{v3} == {v6}");

            Assert.IsTrue(v4 > v1, $"{v4} > {v1}");
            Assert.IsTrue(v4 > v2, $"{v4} > {v2}");
            Assert.IsTrue(v4 > v3, $"{v4} > {v3}");
            Assert.IsTrue(v4 > v5, $"{v4} > {v5}");
            Assert.IsTrue(v4 > v6, $"{v4} > {v6}");
            Assert.IsTrue(v4 < v7, $"{v4} < {v7}");
        }

        [TestMethod]
        public void Test_SemVersion_Compatability()
        {
            var v1 = new SemVersion(2, 0, 254);
            var v2 = new SemVersion(2, 0, 10);
            var v3 = new SemVersion(2, 1, 10);
            var v4 = new SemVersion(3, 0, 10);

            Assert.IsTrue(v1.IsCompatible(v2));
            Assert.IsFalse(v1.IsCompatible(v3));
            Assert.IsFalse(v1.IsCompatible(v4));

            Assert.IsFalse(v2.IsCompatible(v1));
            Assert.IsFalse(v2.IsCompatible(v3));
            Assert.IsFalse(v2.IsCompatible(v4));

            Assert.IsTrue(v3.IsCompatible(v1));
            Assert.IsTrue(v3.IsCompatible(v2));
            Assert.IsFalse(v3.IsCompatible(v4));

            Assert.IsFalse(v4.IsCompatible(v1));
            Assert.IsFalse(v4.IsCompatible(v2));
            Assert.IsFalse(v4.IsCompatible(v3));
            Assert.IsTrue(v4.IsCompatible(v4));
        }

        [TestMethod]
        public void Test_SemVersion_string()
        {
            var v1 = new SemVersion("1.2.3");
            var v2 = new SemVersion("1.2");
            var v3 = new SemVersion("1");
            var v4 = new SemVersion("");
            var v5 = new SemVersion("a1.1.3.re.5");

            Assert.AreEqual(new SemVersion(1, 2, 3), v1);
            Assert.AreEqual(new SemVersion(1, 2, 0), v2);
            Assert.AreEqual(new SemVersion(1, 0, 0), v3);
            Assert.AreEqual(new SemVersion(0, 0, 0), v4);
            Assert.AreEqual(new SemVersion(1, 3, 5), v5);
        }
    }
}
