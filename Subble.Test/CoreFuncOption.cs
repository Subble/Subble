using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Subble.Core.Func.Option;
using Subble.Core.Func;
using System;

namespace Subble.Test
{
    [TestClass]
    public class CoreFuncOption
    {
        private class UnusedClass { }
        private struct UnusedStruct { }

        [TestMethod]
        public void TestOptionMatch()
        {
            const int valInt = 45;
            const float valFloat = 4.5f;
            const double valDouble = 5.4;
            const string valString = "SomeString";

            TestValue(valInt);
            TestValue(valFloat);
            TestValue(valDouble);
            TestValue(valString);

            TestNull<string>();
            TestNull<int>();
            TestNull<UnusedClass>();
        }

        [TestMethod]
        public void TestOptionCast()
        {
            const int valInt = 10;
            var valTime = DateTime.Now;

            double toDouble(int n) => n;
            string toLongString(DateTime t) => t.ToLongDateString();

            var valDouble = toDouble(valInt);
            var valString = toLongString(valTime);

            Assert.AreEqual(10.0, valDouble);
            Assert.IsNotNull(valString);

            var someInt = Some(valInt);
            var someDate = Some(valTime);

            someInt
                .Cast(toDouble)
                .Match(
                    None: Assert.Fail,
                    Some: d => Assert.AreEqual(valDouble, d));

            someDate
                .Cast(toLongString)
                .Match(
                    None: Assert.Fail,
                    Some: s => Assert.AreEqual(valString, s));


            None<int>()
                .Cast(toDouble)
                .Match(
                    None: () => { },
                    Some: _ => Assert.Fail());
        }

        [TestMethod]
        public void TestOptionPatternChain()
        {
            const string val = "DATA";
            Option option = Some(val);

            option
                .Some<string>(s => Assert.AreEqual(val, s))
                .Some<IComparable<String>>(c => Assert.AreEqual(0, c.CompareTo(val)))
                .Some<UnusedClass>(_ => Assert.Fail())
                .None<IComparable<String>>(Assert.Fail)
                .None<string>(Assert.Fail);
        }

        private void TestValue<T>(T val)
        {
            Assert.IsNotNull(val, "Invalid test case, use TestNull");

            Option option = Some(val);
            Option<T> typed = Some(val);

            Assert.IsNotNull(option);
            Assert.IsNotNull(typed);

            Assert.IsTrue(option.HasValue<T>());
            Assert.IsTrue(typed.HasValue());

            Assert.IsTrue(option.HasValue(out T result));
            Assert.IsTrue(typed.HasValue(out var tResult));

            Assert.IsNotNull(result);
            Assert.IsNotNull(tResult);

            Assert.IsInstanceOfType(result, typeof(T));

            Assert.AreEqual(val, result);
            Assert.AreEqual(val, tResult);

            option.Match<T>(
                Assert.Fail,
                res => Assert.AreEqual(val, res));
            typed.Match(
                Assert.Fail,
                res => Assert.AreEqual(val, res));

            Assert.IsFalse(option.HasValue(out UnusedClass invalid));

            //None should be called
            option.Match<UnusedClass>(
                () => { },
                _ => Assert.Fail("Should match None"));
        }

        private void TestNull<T>()
        {
            var option = None();
            var typed = None<T>();

            Assert.IsFalse(option.HasValue<T>());
            Assert.IsFalse(typed.HasValue());

            option.Match<T>(
                () => { },
                _ => Assert.Fail("Should match None"));
            typed.Match(
                () => { },
                _ => Assert.Fail("Should match None"));
        }
    }
}
