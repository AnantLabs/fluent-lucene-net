using System;
using System.Collections.Generic;
using FluentLucene.Materialize;
using FluentLucene.Tests.TestUtils;
using FluentLucene.Types;
using NUnit.Framework;

namespace FluentLucene.Tests.Materialize
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ConvertToConstant.Local

    [Category("Types")]
    [TestFixture]
    public class TypeFactoryTests
    {
        private TypeFactory TypeFactory;

        [SetUp]
        public void SetUp()
        {
            TypeFactory = new TypeFactory();
        }

        private void AssertAllTypesSupported(IEnumerable<Type> types)
        {
            // Lots of logic for a test, but allows for more maintainable tests.

            foreach (var type in types)
            {
                var clrType = type;

                var mappingType = TypeFactory.GetFor(clrType);
                
                Assert.True(
                    clrType.IsAssignableFrom(mappingType.ClrType), 
                    "The CLR type of the mapped type ({0}) is not compatible with {1}", mappingType.ClrType, clrType);
            }
        }

        [Test]
        public void GetFor_NativeType_ReturnsMappingType()
        {
            AssertAllTypesSupported(SupportedTypes.NativeTypes);
        }

        [Test]
        public void GetFor_EnumType_ReturnsMappingType()
        {
            var mappingType = TypeFactory.GetFor(typeof (TypeCode));

            Assert.That(mappingType, Is.TypeOf<EnumType>());
        }

        [Test]
        public void GetFor_NullableNativeType_ReturnsMappingType()
        {
            AssertAllTypesSupported(SupportedTypes.NullableNativeTypes);
        }

        [Test]
        public void GetFor_ComplementaryType_ReturnsMappingType()
        {
            AssertAllTypesSupported(SupportedTypes.ComplementaryType);
        }

        [Test]
        public void GetFor_NullableComplementaryType_ReturnsMappingType()
        {
            AssertAllTypesSupported(SupportedTypes.NullableComplementaryType);
        }

        [Test]
        public void GetFor_TypeNotSupported_ThrowsExceptionWithTypeNameInMessage()
        {
            var expected = Throws
                .TypeOf<TypeNotSupportedException>()
                .With.Message.StringMatching("IAsyncResult");

            Assert.That(() => TypeFactory.GetFor(typeof(IAsyncResult)), expected);
        }

        [Test]
        public void _SmokeDetector_GetFor_AllSupportedTypes_ReturnsNotNull()
        {
            foreach (var type in SupportedTypes.All)
            {
                var clrType = type;

                var mappingType = TypeFactory.GetFor(clrType);

                Assert.That(mappingType, Is.Not.Null);
            }
        }
    }
}