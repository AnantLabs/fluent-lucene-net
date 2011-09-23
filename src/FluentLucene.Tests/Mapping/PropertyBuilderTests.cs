using System;
using System.Linq;
using FluentLucene.Mapping;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using NUnit.Framework;

namespace FluentLucene.Tests.Mapping
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ConvertToConstant.Local

    [Category("Mappings")]
    [TestFixture]
    internal class PropertyBuilderTests : MappingTestsBase
    {
        [Test]
        public void Map_WithoutSpecifyingName_SetsNameToPropertyName()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue));

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().Name, Is.EqualTo("StringValue"));
        }

        [Test]
        public void Map_WithNameSpecified_SetsNameToSpecifiedName()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue, "Blah"));

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().Name, Is.EqualTo("Blah"));
        }

        [Test]
        public void Map_WithField_SetsName()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue).Field("Field"));

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().Name, Is.EqualTo("Field"));
        }

        [Test]
        public void Map_WithNameAndField_FieldOverridesName()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue, "Name").Field("Field"));

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().Name, Is.EqualTo("Field"));
        }

        [Test]
        public void Map_WithMultipleFields_LastFieldWins()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue).Field("Field1").Field("Field2"));

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().Name, Is.EqualTo("Field2"));
        }

        [Test]
        public void Map_WithCustomAnalyzer_SetsAnalyzer()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue).Analyzer<StopAnalyzer>());

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().AnalyzerType, Is.EqualTo(typeof(StopAnalyzer)));
        }

        [Test]
        public void Map_WithCustomAnalyzerNonGeneric_SetsAnalyzer()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue).Analyzer(typeof(StopAnalyzer)));

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().AnalyzerType, Is.EqualTo(typeof(StopAnalyzer)));
        }

        [Test]
        public void Map_WithoutCustomAnalyzer_DefaultsToStandardAnalyzer()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue));

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().AnalyzerType, Is.EqualTo(typeof(StandardAnalyzer)));
        }

        [Test]
        public void Map_WithDefaultAnalyzer_DefaultsToDefaultAnalyzer()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue));
            map.Analyzer<StopAnalyzer>();

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().AnalyzerType, Is.EqualTo(typeof(StopAnalyzer)));
        }

        [Test]
        public void Map_WithDefaultAnalyzerNonGeneric_DefaultsToDefaultAnalyzer()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue));
            map.Analyzer(typeof(StopAnalyzer));

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().AnalyzerType, Is.EqualTo(typeof(StopAnalyzer)));
        }

        [Test]
        public void Map_WithDefaultAnalyzerAndCustomAnalyzer_CustomOverridesDefault()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue).Analyzer<SimpleAnalyzer>());
            map.Analyzer<StopAnalyzer>();

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().AnalyzerType, Is.EqualTo(typeof(SimpleAnalyzer)));
        }

        [Test]
        public void Map_WithoutBoost_DefaultsToOne()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue));

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().Boost, Is.EqualTo(1d));
        }

        [Test]
        public void Map_WithBoost_SetsBoost()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue).Boost(2));

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().Boost, Is.EqualTo(2d));
        }

        [Test]
        public void Map_WithIndexAsValue_SetsIndexValue()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue).Indexed.As(FieldIndex.AsIs));

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().Index, Is.EqualTo(FieldIndex.AsIs));
        }

        [Test]
        public void Map_WithIndexAsIs_SetsIndexToAsIs()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue).Indexed.AsIs());

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().Index, Is.EqualTo(FieldIndex.AsIs));
        }

        [Test]
        public void Map_WithIndexTokenized_SetsIndexToTokenized()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue).Indexed.Tokenized());

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().Index, Is.EqualTo(FieldIndex.Tokenized));
        }

        [Test]
        public void Map_WithIndexNo_SetsIndexToNo()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue).Indexed.No());

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().Index, Is.EqualTo(FieldIndex.No));
        }

        [Test]
        public void Map_WithIndexWithoutNorms_SetsIndexToWithoutNorms()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue).Indexed.WithoutNorms());

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().Index, Is.EqualTo(FieldIndex.WithoutNorms));
        }

        [Test]
        public void Map_WithStoreAsValue_SetsStoreValue()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue).Stored.As(FieldStore.Compressed));

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().Store, Is.EqualTo(FieldStore.Compressed));
        }

        [Test]
        public void Map_WithStoreYes_SetsStoreToYes()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue).Stored.Yes());

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().Store, Is.EqualTo(FieldStore.Yes));
        }

        [Test]
        public void Map_WithStoreNo_SetsStoreToNo()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue).Stored.No());

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().Store, Is.EqualTo(FieldStore.No));
        }

        [Test]
        public void Map_WithStoreCompressed_SetsStoreToCompressed()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue).Stored.Compressed());

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().Store, Is.EqualTo(FieldStore.Compressed));
        }

        [Test]
        public void Map_Sortable_SetsIsSortableToTrue()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue).Sortable());

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().IsSortable, Is.True);
        }

        [Test]
        public void Map_NotSortable_SetsIsSortableToFalse()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue).Not.Sortable());

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().IsSortable, Is.False);
        }

        [Test]
        public void Map_WithDefaultSortableWithoutCustomSortable_SetsSortableToDefault()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue));
            map.Sortable.DefaultsTo.Sortable();

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().IsSortable, Is.True);
        }

        [Test]
        public void Map_SortableNotSpecified_DefaultsToFalse()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue));

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().IsSortable, Is.False);
        }

        [Test]
        public void Map_WithDefaultSortableAndCustomSortable_CustomOverridesDefault()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue).Not.Sortable());
            map.Sortable.DefaultsTo.Sortable();

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().IsSortable, Is.False);
        }

        [Test]
        public void Map_UsingNotThenCallingAnotherInvertableOperation_ResetsNotAfterUsage()
        {
            var map = GetMappings(m => m.Map(x => x.StringValue).Not.Sortable().Sortable());

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().IsSortable, Is.True);
        }

        [Test]
        public void Map_ExpressionWithConvertionInIt_FindsMember()
        {
            var map = GetMappings(m => m.Map<object>(x => x.Id));

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().Name, Is.EqualTo("Id"));
        }

        [Test]
        public void Map_ExpressionWithMethodsCall_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => GetMappings(m => m.Map(x => x.MethodCall())));
        }

        [Test]
        public void Map_ExpressionNotRepresentingAMember_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => GetMappings(m => m.Map(x => x.StringValue + "blah")));
        }

        [Test]
        public void Map_ExpressionRepresentsField_FindsMember()
        {
            var map = GetMappings(m => m.Map(x => x.FieldPublic));

            var model = ToMappingModel(map);

            Assert.That(model.Fields.Single().Name, Is.EqualTo("FieldPublic"));
        }

        [Test]
        public void Map_PropertyWithPublicGet_CanGetValue()
        {
            var map = GetMappings(m => m.Map(x => x.PropertyPublic));
            var model = ToMappingModel(map);
            var field = model.Fields.Single();
            var target = new SampleDocument { PropertyPublic = "My value" };

            var value = field.GetValue(target);

            Assert.That(value, Is.EqualTo("My value"));
        }

        [Test]
        public void Map_PropertyWithInternalGet_CanGetValue()
        {
            var map = GetMappings(m => m.Map(x => x.PropertyInternal));
            var model = ToMappingModel(map);
            var field = model.Fields.Single();
            var target = new SampleDocument { PropertyInternal = "My value" };

            var value = field.GetValue(target);

            Assert.That(value, Is.EqualTo("My value"));
        }

        [Test]
        public void Map_PropertyWithPublicSet_CanSetValue()
        {
            var map = GetMappings(m => m.Map(x => x.PropertyPublic));
            var model = ToMappingModel(map);
            var field = model.Fields.Single();
            var target = new SampleDocument();

            field.SetValue(target, "This is new");

            Assert.That(target.PropertyPublic, Is.EqualTo("This is new"));
        }

        [Test]
        public void Map_InternalProperty_CanSetValue()
        {
            var map = GetMappings(m => m.Map(x => x.PropertyInternal));
            var model = ToMappingModel(map);
            var field = model.Fields.Single();
            var target = new SampleDocument();

            field.SetValue(target, "This is new");

            Assert.That(target.PropertyInternal, Is.EqualTo("This is new"));
        }

        [Test]
        public void Map_InternalPropertyPrivateSet_CanSetValue()
        {
            var map = GetMappings(m => m.Map(x => x.PropertyInternalPrivate));
            var model = ToMappingModel(map);
            var field = model.Fields.Single();
            var target = new SampleDocument();

            field.SetValue(target, "This is new");

            Assert.That(target.PropertyInternalPrivate, Is.EqualTo("This is new"));
        }

        [Test]
        public void Map_PublicField_CanGetValue()
        {
            var map = GetMappings(m => m.Map(x => x.FieldPublic));
            var model = ToMappingModel(map);
            var field = model.Fields.Single();
            var target = new SampleDocument { FieldPublic = "My value" };

            var value = field.GetValue(target);

            Assert.That(value, Is.EqualTo("My value"));
        }

        [Test]
        public void Map_InternalField_CanGetValue()
        {
            var map = GetMappings(m => m.Map(x => x.FieldInternal));
            var model = ToMappingModel(map);
            var field = model.Fields.Single();
            var target = new SampleDocument { FieldInternal = "My value" };

            var value = field.GetValue(target);

            Assert.That(value, Is.EqualTo("My value"));
        }

        [Test]
        public void Map_PublicField_CanSetValue()
        {
            var map = GetMappings(m => m.Map(x => x.FieldPublic));
            var model = ToMappingModel(map);
            var field = model.Fields.Single();
            var target = new SampleDocument();

            field.SetValue(target, "This is new");

            Assert.That(target.FieldPublic, Is.EqualTo("This is new"));
        }

        [Test]
        public void Map_InternalField_CanSetValue()
        {
            var map = GetMappings(m => m.Map(x => x.FieldInternal));
            var model = ToMappingModel(map);
            var field = model.Fields.Single();
            var target = new SampleDocument();

            field.SetValue(target, "This is new");

            Assert.That(target.FieldInternal, Is.EqualTo("This is new"));
        }
    }
}