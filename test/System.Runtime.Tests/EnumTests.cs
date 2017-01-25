using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Runtime.Tests
{
    [TestClass]
    public class EnumTests
    {
        private readonly Type TypeTestEnum = typeof(TypeCode);

        private IEnumerable<int> GetValues()
        {
            yield return (int)TypeCode.Empty;
            yield return (int)TypeCode.Object;
            yield return (int)TypeCode.DBNull;
            yield return (int)TypeCode.Boolean;
            yield return (int)TypeCode.Char;
            yield return (int)TypeCode.SByte;
            yield return (int)TypeCode.Byte;
            yield return (int)TypeCode.Int16;
            yield return (int)TypeCode.UInt16;
            yield return (int)TypeCode.Int32;
            yield return (int)TypeCode.UInt32;
            yield return (int)TypeCode.Int64;
            yield return (int)TypeCode.UInt64;
            yield return (int)TypeCode.Single;
            yield return (int)TypeCode.Double;
            yield return (int)TypeCode.Decimal;
            yield return (int)TypeCode.DateTime;
            yield return (int)TypeCode.String;
            yield return 200;
        }

        private IEnumerable<string> GetNames()
        {
            yield return nameof(TypeCode.Empty);
            yield return nameof(TypeCode.Object);
            yield return nameof(TypeCode.DBNull);
            yield return nameof(TypeCode.Boolean);
            yield return nameof(TypeCode.Char);
            yield return nameof(TypeCode.SByte);
            yield return nameof(TypeCode.Byte);
            yield return nameof(TypeCode.Int16);
            yield return nameof(TypeCode.UInt16);
            yield return nameof(TypeCode.Int32);
            yield return nameof(TypeCode.UInt32);
            yield return nameof(TypeCode.Int64);
            yield return nameof(TypeCode.UInt64);
            yield return nameof(TypeCode.Single);
            yield return nameof(TypeCode.Double);
            yield return nameof(TypeCode.Decimal);
            yield return nameof(TypeCode.DateTime);
            yield return nameof(TypeCode.String);
            yield return "200";
        }

        [TestMethod]
        public void EnumFormat()
        {
            int[] values = GetValues().ToArray();
            string[] names = GetNames().ToArray();

            for (int i = 0; i < values.Length; i++)
            {
                string fmtG = Mock.System.Enum2.Format(TypeTestEnum, values[i], "g");
                string fmtG2 = Mock.System.Enum2.Format(TypeTestEnum, values[i], "G");
                string fmtX = Mock.System.Enum2.Format(TypeTestEnum, values[i], "x");
                string fmtX2 = Mock.System.Enum2.Format(TypeTestEnum, values[i], "X");
                string fmtD = Mock.System.Enum2.Format(TypeTestEnum, values[i], "d");
                string fmtD2 = Mock.System.Enum2.Format(TypeTestEnum, values[i], "D");
                string fmtF = Mock.System.Enum2.Format(TypeTestEnum, values[i], "f");
                string fmtF2 = Mock.System.Enum2.Format(TypeTestEnum, values[i], "F");

                Assert.IsNotNull(fmtG);
                Assert.IsNotNull(fmtG2);
                Assert.IsNotNull(fmtX);
                Assert.IsNotNull(fmtX2);
                Assert.IsNotNull(fmtD);
                Assert.IsNotNull(fmtD2);
                Assert.IsNotNull(fmtF);
                Assert.IsNotNull(fmtF2);

                Assert.AreEqual(fmtG, fmtG2);
                Assert.AreEqual(fmtX, fmtX2);
                Assert.AreEqual(fmtD, fmtD2);
                Assert.AreEqual(fmtF, fmtF2);

                Assert.AreEqual(names[i], fmtG);
                Assert.AreEqual(values[i].ToString("X8"), fmtX);
                Assert.AreEqual(values[i].ToString(), fmtD);
                Assert.AreEqual(names[i], fmtF);
            }
        }

        [TestMethod]
        public void EnumGetNames()
        {
            string[] result = Mock.System.Enum2.GetNames(TypeTestEnum);
            string[] expected = GetNames().ToArray();
            expected = expected.Take(expected.Length - 1).ToArray();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected.Length, result.Length);

            for (int i = 0; i < result.Length; i++)
            {
                Assert.AreEqual(expected[i], result[i]);
            }
        }

        [TestMethod]
        public void EnumGetValues()
        {
            Array result = Mock.System.Enum2.GetValues(TypeTestEnum);
            int[] expected = GetValues().ToArray();
            expected = expected.Take(expected.Length - 1).ToArray();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected.Length, result.Length);

            for (int i = 0; i < result.Length; i++)
            {
                Assert.AreEqual(expected[i], Convert.ToInt64(result.GetValue(i)));
            }
        }
    }
}
