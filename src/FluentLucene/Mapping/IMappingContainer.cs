using System;
using System.Collections.Generic;
using FluentLucene.MappingModel;

namespace FluentLucene.Mapping
{
    /// <summary>
    /// Represents a container of mappings
    /// </summary>
    internal interface IMappingContainer
    {
        /// <summary>
        /// Gets the mappings stored in this container
        /// </summary>
        IEnumerable<DocumentMapping> Mappings { get; }

        /// <summary>
        /// Gets the mappings for a given type. If no mapping exist for the given type, this method
        /// returns null.
        /// </summary>
        /// <param name="type">The type for which to get the mappings</param>
        /// <returns>The mappings or null</returns>
        DocumentMapping GetMappingsFor(Type type);
    }
}