using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentLucene.Mapping;
using FluentLucene.MappingModel;
using FluentLucene.Materialize;
using Lucene.Net.Documents;
using NUnit.Framework;

namespace FluentLucene.Tests.Materialize
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ConvertToConstant.Local

    [Category("Materialize")]
    [TestFixture]
    public class SimpleMaterializerTests
    {
        private SimpleMaterializer Materializer;

        [SetUp]
        public void SetUp()
        {
            Materializer = new SimpleMaterializer();
        }

        [Test]
        public void Materialize_IdentityMapping_MapsIdentityField()
        {
            // Arrange
            var mappings = CreateMappingForIdentity(x => x.Id);
            
            var document = new Document();
            document.Add(CreateField("Id", "1"));

            // Act
            var actual = (Person)Materializer.Materialize(mappings, document);

            // Assert
            Assert.That(actual.Id, Is.EqualTo(1));
        }

        [Test]
        public void Materialize_IdentityMappingWithDifferentName_MapsIdentityField()
        {
            // Arrange
            var mappings = CreateMappingForIdentity(x => x.Id, "MyId");

            var document = new Document();
            document.Add(CreateField("MyId", "1"));

            // Act
            var actual = (Person)Materializer.Materialize(mappings, document);

            // Assert
            Assert.That(actual.Id, Is.EqualTo(1));
        }

        [Test]
        public void Materialize_FieldMapping_MapsField()
        {
            // Arrange
            var mappings = CreateMapping(x => x.Name);

            var document = new Document();
            document.Add(CreateField("Name", "Per Son"));

            // Act
            var actual = (Person)Materializer.Materialize(mappings, document);

            // Assert
            Assert.That(actual.Name, Is.EqualTo("Per Son"));
        }

        [Test]
        public void Materialize_FieldMappingWithDifferentName_MapsField()
        {
            // Arrange
            var mappings = CreateMapping(x => x.Name, "MyName");

            var document = new Document();
            document.Add(CreateField("MyName", "Per Son"));

            // Act
            var actual = (Person)Materializer.Materialize(mappings, document);

            // Assert
            Assert.That(actual.Name, Is.EqualTo("Per Son"));
        }

        [Test]
        public void Materialize_FieldMappingWithNameEqualToOtherMappedProperty_DoesNotInvokeOtherProperty()
        {
            // Arrange
            var mappings = CreateMapping(x => x.Name, "OtherName",
                      CreateFieldMapping(x => x.OtherName, "MyOtherName"));

            var document = new Document();
            document.Add(CreateField("OtherName", "Per Son"));

            // Act
            var actual = (Person)Materializer.Materialize(mappings, document);

            // Assert
            Assert.That(actual.OtherNameInvoked, Is.False);
            Assert.That(actual.Name, Is.EqualTo("Per Son"));
        }

        [Test]
        public void Materialize_PropertyNotMapped_PropertyIsNotInvoked()
        {
            // Arrange
            var mappings = CreateMapping(x => x.Name);

            var document = new Document();
            document.Add(CreateField("Name", "Per Son"));

            // Act
            var actual = (Person)Materializer.Materialize(mappings, document);

            // Assert
            Assert.That(actual.OtherNameInvoked, Is.False);
        }

        [Test]
        public void Materialize_MappedPropertyFieldNotPresent_PropertyIsNotInvoked()
        {
            // Arrange
            var mappings = CreateMapping(x => x.OtherName);
            var document = new Document();

            // Act
            var actual = (Person)Materializer.Materialize(mappings, document);

            // Assert
            Assert.That(actual.OtherNameInvoked, Is.False);
        }

        [Test]
        public void Materialize_MappedPropertyFieldPresentTwice_FirstFieldIsConsidered()
        {
            // Arrange
            var mappings = CreateMapping(x => x.Name);

            var document = new Document();
            document.Add(CreateField("Name", "Per Son"));
            document.Add(CreateField("Name", "Alter Ego"));

            // Act
            var actual = (Person)Materializer.Materialize(mappings, document);

            // Assert
            Assert.That(actual.Name, Is.EqualTo("Per Son"));
        }

        #region Helpers

        private static DocumentMapping CreateMappingForIdentity(Expression<Func<Person, object>> expression, string name = null)
        {
            var identity = new IdentityMapping(CreateMember(expression));
            if (name != null) identity.Name = name;

            return new DocumentMapping(typeof(Person), identity, Enumerable.Empty<FieldMapping>());
        }

        private static DocumentMapping CreateMapping(Expression<Func<Person, object>> expression, string name = null, params FieldMapping[] otherMappings)
        {
            var identity = new IdentityMapping(CreateMember(x => x.Id));
            var fields = new[] { CreateFieldMapping(expression, name) }.Concat(otherMappings);
            return new DocumentMapping(typeof(Person), identity, fields);
        }

        private static FieldMapping CreateFieldMapping(Expression<Func<Person, object>> expression, string name = null)
        {
            var field = new FieldMapping(CreateMember(expression));
            if (name != null) field.Name = name;
            return field;
        }

        private static Field CreateField(string name, string value)
        {
            return new Field(name, value, Field.Store.YES, Field.Index.ANALYZED);
        }

        private static PropertyMember CreateMember(Expression<Func<Person, object>> expression)
        {
            var memberInfo = expression.ToMemberExpression().Member;
            return new PropertyMember((PropertyInfo)memberInfo);
        }

        private class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public bool OtherNameInvoked { get; private set; }
            private string otherName;
            public string OtherName
            {
                get
                {
                    OtherNameInvoked = true;
                    return otherName;
                }
                set
                {
                    OtherNameInvoked = true;
                    otherName = value;
                }
            }
        }

        #endregion
    }
}