using System;
using FluentLucene.MappingModel;
using Lucene.Net.Documents;
using System.Linq;

namespace FluentLucene.Materialize
{
    /// <summary>
    /// Implementation of an activator used to create entities.
    /// </summary>
    internal class EntityActivator
    {
        /// <summary>
        /// Creates an instance of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of the entity to instantiate</typeparam>
        /// <returns>The newly created entity</returns>
        public T CreateInstance<T>()
        {
            return (T)Activator.CreateInstance(typeof (T));
        }

        /// <summary>
        /// Creates an instance of the specified type
        /// </summary>
        /// <param name="type">Type type of the entity to instantiate</param>
        /// <returns>The newly created entity</returns>
        public object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }

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