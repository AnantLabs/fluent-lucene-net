using System;
using NUnit.Framework;

namespace FluentLucene.Tests.Mapping
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ConvertToConstant.Local

    [Category("Mappings")]
    [TestFixture]
    internal class IdentityBuilderTests : MappingTestsBase
    {
        [Test]
        public void Id_WithoutSpecifyingName_SetsNameToPropertyName()
        {
            var map = GetMappings(m => m.Id(x => x.Id));

            var model = ToMappingModel(map);

            Assert.That(model.Identity.Name, Is.EqualTo("Id"));
        }

        [Test]
        public void Id_WithNameSpecified_SetsNameToSpecifiedName()
        {
            var map = GetMappings(m => m.Id(x => x.Id, "Blah"));

            var model = ToMappingModel(map);

            Assert.That(model.Identity.Name, Is.EqualTo("Blah"));
        }

        [Test]
        public void Id_WithField_SetsName()
        {
            var map = GetMappings(m => m.Id(x => x.Id).Field("Field"));

            var model = ToMappingModel(map);

            Assert.That(model.Identity.Name, Is.EqualTo("Field"));
        }

        [Test]
        public void Id_WithNameAndField_FieldOverridesName()
        {
            var map = GetMappings(m => m.Id(x => x.Id, "Name").Field("Field"));

            var model = ToMappingModel(map);

            Assert.That(model.Identity.Name, Is.EqualTo("Field"));
        }

        [Test]
        public void Id_WithMultipleFields_LastFieldWins()
        {
            var map = GetMappings(m => m.Id(x => x.Id).Field("Field1").Field("Field2"));

            var model = ToMappingModel(map);

            Assert.That(model.Identity.Name, Is.EqualTo("Field2"));
        }

        [Test]
        public void Id_DefinedTwice_ThrowInvalidOperationException()
        {
            var map = GetMappings(m => m.Id(x => x.Id));

            Assert.Throws<InvalidOperationException>(() => map.Id(x => x.StringValue));
        }
    }
}