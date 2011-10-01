using System;
using System.Globalization;
using FluentLucene.MappingModel;
using Lucene.Net.Documents;

namespace FluentLucene.Materialize
{
    /// <summary>
    /// Basic implementation of a materializer
    /// </summary>
    internal class SimpleMaterializer : IMaterializer
    {
        private readonly IEntityActivator Activator;
        private readonly ITypeFactory TypeFactory;

        public SimpleMaterializer(IEntityActivator activator, ITypeFactory typeFactory)
        {
            Activator = activator;
            TypeFactory = typeFactory;
        }

        /// <summary>
        /// Materializes an entity from mappings and a given document
        /// </summary>
        /// <param name="mapping">The mappings describing how materialize the document</param>
        /// <param name="document">The Lucene document containing information to materialize</param>
        /// <returns>The materialized entity</returns>
        public object Materialize(DocumentMapping mapping, Document document)
        {
            // Create a new object to hydrate
            var result = Activator.CreateInstance(mapping.DocumentType);

            // Set the value of all mapped fields
            foreach (var map in mapping.AllFieldLikeMappings)
            {
                var field = document.GetFieldable(map.Name);

                if (field != null)
                {
                    var value = ParseValue(field, map.Type);
                    map.SetValue(result, value);
                }
            }

            return result;
        }

        /// <summary>
        /// Parses a string value into the correct type.
        /// </summary>
        /// <param name="field">The field containing the value</param>
        /// <param name="mappingType">The expected type of the value</param>
        /// <returns>The parsed value</returns>
        /// <exception cref="TypeNotSupportedException">If the type of the value is not supported</exception>
        public object ParseValue(Fieldable field, Type mappingType)
        {
            var type = TypeFactory.GetFor(mappingType);
            return type.GetValue(field);
        }
    }
}