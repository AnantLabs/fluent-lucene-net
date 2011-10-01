using System;
using FluentLucene.Mapping;
using FluentLucene.MappingModel;
using FluentLucene.Materialize;
using Moq;
using NUnit.Framework;

namespace FluentLucene.Tests.Mapping
{
    internal abstract class MappingTestsBase
    {
        protected Mock<ITypeProvider> TypeProviderMock;
        private IMappingFactory MappingFactory;

        [SetUp]
        public void SetUp()
        {
            TypeProviderMock = new Mock<ITypeProvider>();
            MappingFactory = new MappingFactory(TypeProviderMock.Object);
        }

        protected SearchMap<SampleDocument> GetMappings(Action<SearchMap<SampleDocument>> mappings) 
        {
            var map = new SearchMap<SampleDocument>();
            mappings(map);
            if (map.Identity == null) map.Id(x => x.Id);

            return map;
        }

        protected SearchMap<T> MappingsFor<T>()
        {
            return new SearchMap<T>();
        }

        protected DocumentMapping ToMappingModel(ISearchMap map)
        {
            return MappingFactory.CreateDocument(map);
        }
    }
}