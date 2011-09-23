using System;
using FluentLucene.Materialize;
using NUnit.Framework;

namespace FluentLucene.Tests.Materialize
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ConvertToConstant.Local

    [Category("Materialize")]
    [TestFixture]
    public class SimpleMaterializerTests
    {
        private void AssertParseValue<T>(string stringValue, T expected)
        {
            var materializer = new SimpleMaterializer();
            var actual = materializer.ParseValue(stringValue, typeof(T));

            if (expected == null)
            {
                Assert.That(actual, Is.Null);
                return;
            }
            else
            {
                Assert.That(actual, Is.Not.Null);

                var actualType = actual.GetType();
                var expectedType = typeof (T);

                if (expectedType.IsGenericType)
                {
                    var genericType = expectedType.GetGenericTypeDefinition();

                    if (genericType == typeof(Nullable<>))
                    {
                        expectedType = Nullable.GetUnderlyingType(expectedType);
                    }
                }

                Assert.That(actualType, Is.EqualTo(expectedType));
            }
        }

        [Test]
        public void ParseValue_Int_ReturnsValue()
        {
            AssertParseValue("15", 15);
        }

        [Test]
        public void ParseValue_NullableWithValue_ReturnsValue()
        {
            AssertParseValue<int?>("15", 15);
        }

        [Test]
        public void ParseValue_NullableNull_ReturnsNull()
        {
            AssertParseValue<int?>("", null);
        }

        [Test]
        public void ParseValue_String_ReturnsValue()
        {
            AssertParseValue("15", "15");
        }

        [Test]
        public void ParseValue_Double_ReturnsValue()
        {
            AssertParseValue("15.1", 15.1);
        }

        [Test]
        public void ParseValue_Float_ReturnsValue()
        {
            AssertParseValue("15.1", 15.1f);
        }

        [Test]
        public void ParseValue_Boolean_ReturnsValue()
        {
            AssertParseValue("true", true);
        }

        [Test]
        public void ParseValue_Short_ReturnsValue()
        {
            AssertParseValue("15", (short)15);
        }

        [Test]
        public void ParseValue_Long_ReturnsValue()
        {
            AssertParseValue("15", (long)15);
        }

        [Test]
        public void ParseValue_UInt_ReturnsValue()
        {
            AssertParseValue("15", (uint)15);
        }

        [Test]
        public void ParseValue_UShort_ReturnsValue()
        {
            AssertParseValue("15", (ushort)15);
        }

        [Test]
        public void ParseValue_ULong_ReturnsValue()
        {
            AssertParseValue("15", (ulong)15);
        }

        [Test]
        public void ParseValue_Byte_ReturnsValue()
        {
            AssertParseValue("15", (byte)15);
        }

        [Test]
        public void ParseValue_SByte_ReturnsValue()
        {
            AssertParseValue("15", (sbyte)15);
        }

        [Test]
        public void ParseValue_Char_ReturnsValue()
        {
            AssertParseValue("$", '$');
        }

        [Test]
        public void ParseValue_DateTime_ReturnsValue()
        {
            AssertParseValue(
                "2009-06-15T20:45:30.0900019",
                new DateTime(2009, 6, 15, 20, 45, 30).AddTicks(19));
        }

        [Test]
        public void ParseValue_TimeSpan_ReturnsValue()
        {
            AssertParseValue(
                "3.17:25:30.5000019",
                new TimeSpan(3, 17, 25, 30, 500).Add(TimeSpan.FromTicks(19)));
        }

        [Test]
        public void ParseValue_Decimal_ReturnsValue()
        {
            AssertParseValue("15.1", 15.1m);
        }

        [Test]
        public void ParseValue_Enum_ReturnsValue()
        {
            AssertParseValue("Boolean", TypeCode.Boolean);
        }

        [Test]
        public void ParseValue_EnumFlagsInt_ReturnsValue()
        {
            AssertParseValue("68", AttributeTargets.Class | AttributeTargets.Method);
        }

        [Test]
        public void ParseValue_EnumFlagsString_ReturnsValue()
        {
            AssertParseValue("Class, Method", AttributeTargets.Class | AttributeTargets.Method);
        }

        [Test]
        public void ParseValue_UnsupportedType_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(
                () => new SimpleMaterializer().ParseValue("http://www.example.com", typeof (Uri)));
        }

    }
}