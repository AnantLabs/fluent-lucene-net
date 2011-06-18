using System;
using System.Collections.Generic;

namespace FluentLucene.Mapping
{
    /// <summary>
    /// Represents the mapping of a single document
    /// </summary>
    public interface ISearchMap
    {
        /// <summary>
        /// Gets the type of the document
        /// </summary>
        Type DocumentType { get; }

        /// <summary>
        /// Gets the identity current identity builder
        /// </summary>
        IdentityBuilder Identity { get; }

        /// <summary>
        /// The list of mappings defined
        /// </summary>
        IList<PropertyBuilder> Mappings { get; }

        /// <summary>
        /// Get the type of the default analyzer
        /// </summary>
        Type AnalyzerType { get; }

        /// <summary>
        /// Gets the builder for default index configuration
        /// </summary>
        IndexBuilder Index { get; }

        /// <summary>
        /// Gets the builder for default storage configuration
        /// </summary>
        StoreBuilder Storage { get; }

        /// <summary>
        /// Gets the builder for default sorting configuration
        /// </summary>
        SortBuilder Sortable { get; }
    }
}