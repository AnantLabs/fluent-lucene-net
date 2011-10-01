using System;
using FluentLucene.Infrastructure;
using FluentLucene.Materialize;
using FluentLucene.Types;
using Lucene.Net.Documents;
using Moq;
using NUnit.Framework;

namespace FluentLucene.Tests.Materialize
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ConvertToConstant.Local

    [Category("Materialize")]
    [TestFixture]
    public class SimpleMaterializerParseValueTests
    {
        private SimpleMaterializer Materializer;

        [SetUp]
        public void SetUp()
        {
            Materializer = new SimpleMaterializer(new EntityActivator(), new TypeFactory(new ServiceLocator()));
        }

        private Field CreateField(string stringValue)
        {
            return new Field("AnyNameWillDo", stringValue, Field.Store.YES, Field.Index.ANALYZED);
        }

        private void AssertParseValue<T>(string stringValue, T expected)
        {
            var field = CreateField(stringValue);
            var actual = Materializer.ParseValue(field, typeof(T));

            Assert.That(actual, Is.TypeOf<T>());
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ParseValue_Int_ReturnsValue()
        {
            AssertParseValue("15", 15);
        }

        [Test]
        public void ParseValue_NullableWithValue_ReturnsValue()
        {
            var actual = Materializer.ParseValue(CreateField("15"), typeof(int?));

            Assert.That(actual, Is.TypeOf<int>().Or.TypeOf<int?>());
            Assert.That(actual, Is.EqualTo(15));
        }

        [Test]
        public void ParseValue_NullableEmptyString_ReturnsNull()
        {
            var actual = Materializer.ParseValue(CreateField(""), typeof(int?));

            Assert.That(actual, Is.Null);
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
                new DateTime(2009, 6, 15, 20, 45, 30, 90).AddTicks(19));
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
            AssertParseValue("Empty", TypeCode.Empty);
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
            var field = CreateField("http://www.example.com");

            Assert.Throws<TypeNotSupportedException>(() => Materializer.ParseValue(field, typeof(Uri)));
        }

        [Test]
        public void ParseValue_Always_UsesTypeFactoryToMapTypes()
        {
            // Arrange
            var typeFactoryMock = new Mock<ITypeFactory>();
            var materializer = new SimpleMaterializer(new EntityActivator(), typeFactoryMock.Object);

            typeFactoryMock.Setup(x => x.GetFor(typeof (int))).Returns(new Int64Type()).Verifiable();

            // Act
            var value = materializer.ParseValue(CreateField("15"), typeof (int));

            // Assert
            typeFactoryMock.Verify();
            Assert.That(value, Is.TypeOf<long>());
        }
    }
}