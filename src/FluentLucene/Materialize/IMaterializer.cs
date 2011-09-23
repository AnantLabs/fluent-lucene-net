using FluentLucene.MappingModel;
using Lucene.Net.Documents;

namespace FluentLucene.Materialize
{
    /// <summary>
    /// Represents a materializer able to construct objects from mappings and from a Lucene document
    /// </summary>
    internal interface IMaterializer
    {
        /// <summary>
        /// Materializes an entity from mappings and a given document
        /// </summary>
        /// <param name="mapping">The mappings describing how materialize the document</param>
        /// <param name="document">The Lucene document containing information to materialize</param>
        /// <returns>The materialized entity</returns>
        object Materialize(DocumentMapping mapping, Document document);
    }
}