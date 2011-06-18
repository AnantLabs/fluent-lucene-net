using System;
using FluentLucene.MappingModel;
using Lucene.Net.Documents;
using System.Linq;

namespace FluentLucene.Materialize
{
    /// <summary>
    /// Basic implementation of a materializer
    /// </summary>
    internal class BasicMaterializer : IMaterializer
    {
        /// <summary>
        /// Materializes an entity from mappings and a given document
        /// </summary>
        /// <param name="mapping">The mappings describing how materialize the document</param>
        /// <param name="document">The Lucene document containing information to materialize</param>
        /// <returns>The materialized entity</returns>
        public object Materialize(DocumentMapping mapping, Document document)
        {
            var fields = document.GetFields().Cast<Field>();
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Represents a materializer able to instantiate objects from mappings and from a Lucene document
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