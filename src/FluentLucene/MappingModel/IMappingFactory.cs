using FluentLucene.Mapping;

namespace FluentLucene.MappingModel
{
    /// <summary>
    /// Represents a factory able to create mappings
    /// </summary>
    internal interface IMappingFactory
    {
        /// <summary>
        /// Creates mappings for a document given a search map
        /// </summary>
        /// <param name="map">The mappings</param>
        /// <returns>The newly created document mappings</returns>
        DocumentMapping CreateDocument(ISearchMap map);

        /// <summary>
        /// Creates mappings for the identity field given a builder
        /// </summary>
        /// <param name="builder">The builder for the identity field</param>
        /// <returns>The newly create identity mappings</returns>
        IdentityMapping CreateIdentity(IdentityBuilder builder);

        /// <summary>
        /// Creates mappings for a field given a builder and a search map for default values
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <param name="map">The mappings</param>
        /// <returns>The newly created field mappings</returns>
        FieldMapping CreateField(PropertyBuilder builder, ISearchMap map);
    }
}