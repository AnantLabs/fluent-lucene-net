using FluentLucene.Mapping;
using NUnit.Framework;

namespace FluentLucene.Tests.Mapping
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ConvertToConstant.Local

    [Category("Mappings")]
    [TestFixture]
    internal class SortBuilderTests : MappingTestsBase
    {
        private static SortBuilder<object> CreateBuilder()
        {
            return new SortBuilder<object>(null);
        }

        [Test]
        public void Sortable_WithNotCalledNever_SetsIsSortableToTrue()
        {
            var builder = CreateBuilder();

            builder.Sortable();

            Assert.That(builder.IsSortable, Is.True);
        }

        [Test]
        public void Sortable_WithNotCalledOnce_SetsIsSortableToFalse()
        {
            var builder = CreateBuilder();

            builder.Not.Sortable();

            Assert.That(builder.IsSortable, Is.False);
        }

        [Test]
        public void Sortable_WithNotCalledTwice_SetsIsSortableToTrue()
        {
            var builder = CreateBuilder();

            builder.Not.Not.Sortable();

            Assert.That(builder.IsSortable, Is.True);
        }

        [Test]
        public void Sortable_WithNotCalled_ResetsNotForNextCall()
        {
            var builder = CreateBuilder();

            builder.Not.Sortable();
            builder.Sortable();

            Assert.That(builder.IsSortable, Is.True);
        }

    }
}