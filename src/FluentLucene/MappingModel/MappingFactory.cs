using System.Linq;
using FluentLucene.Mapping;
using FluentLucene.Materialize;
using Lucene.Net.Analysis.Standard;

namespace FluentLucene.MappingModel
{
    /// <summary>
    /// A factory able to construct the mapping for a document
    /// </summary>
    internal class MappingFactory : IMappingFactory
    {
        private readonly ITypeFactory TypeFactory;

        public MappingFactory(ITypeFactory typeFactory)
        {
            TypeFactory = typeFactory;
        }

        /// <summary>
        /// Creates mappings for a document given a search map
        /// </summary>
        /// <param name="map">The mappings</param>
        /// <returns>The newly created document mappings</returns>
        public DocumentMapping CreateDocument(ISearchMap map)
        {
            // Create the identity mappings
            var identity = CreateIdentity(map.Identity);

            // Create the fields mappings
            var fields = map.Mappings.Select(x => CreateField(x, map));

            // Create a new document mappings
            var document = new DocumentMapping(map.DocumentType, identity, fields);

            // TODO : Set the fields searchable by default

            // Return the newly created document
            return document;
        }

        /// <summary>
        /// Creates mappings for the identity field given a builder
        /// </summary>
        /// <param name="builder">The builder for the identity field</param>
        /// <returns>The newly create identity mappings</returns>
        public IdentityMapping CreateIdentity(IdentityBuilder builder)
        {
            // Create the identity from the member backing the identity builder
            var identity = new IdentityMapping(builder.Member);

            // Set additionnal values
            identity.Name = builder.FieldName ?? builder.Member.Name;

            // Find the mapping type
            identity.MappingType = TypeFactory.GetFor(builder.Member.MemberType);

            // Return the newly created identity
            return identity;
        }

        /// <summary>
        /// Creates mappings for a field given a builder and a search map for default values
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <param name="map">The mappings</param>
        /// <returns>The newly created field mappings</returns>
        public FieldMapping CreateField(PropertyBuilder builder, ISearchMap map)
        {
            // Create the field from the member backing the property builder
            var field = new FieldMapping(builder.Member);

            // Set additionnal values
            field.AnalyzerType = builder.AnalyzerType ?? map.AnalyzerType ?? typeof(StandardAnalyzer);
            field.Boost = builder.BoostValue ?? 1;
            field.Index = builder.Indexed.Index ?? map.Index.Index ?? FieldIndex.Tokenized;
            field.IsSortable = builder.IsSortable ?? map.Sortable.IsSortable ?? false;
            field.Name = builder.FieldName ?? builder.Member.Name;
            field.Store = builder.Stored.Store ?? map.Storage.Store ?? FieldStore.Yes;

            // Find the mapping type
            field.MappingType = TypeFactory.GetFor(builder.Member.MemberType);

            // Return the newly created field
            return field;
        }
    }
}