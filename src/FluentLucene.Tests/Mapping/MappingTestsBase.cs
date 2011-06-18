using System;
using FluentLucene.Mapping;
using FluentLucene.MappingModel;

namespace FluentLucene.Tests.Mapping
{
    internal abstract class MappingTestsBase
    {
        private readonly IMappingFactory MappingFactory = new MappingFactory();

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