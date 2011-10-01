using System;
using FluentLucene.Mapping;

namespace FluentLucene.MappingModel
{
    /// <summary>
    /// The mapping repesenting a field in a document
    /// </summary>
    internal class FieldMapping : FieldLikeMappingBase
    {
        /// <summary>
        /// Creates a field mapping backed by the specified member
        /// </summary>
        /// <param name="member">The member backing this field</param>
        public FieldMapping(IMember member)
            : base(member)
        { }

        /// <summary>
        /// Get or sets whether the field is sortable or not
        /// </summary>
        public bool IsSortable { get; set; }

        /// <summary>
        /// Get or sets whether the field is searchable by default or not
        /// </summary>
        public bool IsSearchableByDefault { get; set; }

        /// <summary>
        /// Gets or sets how to index the field
        /// </summary>
        public FieldIndex Index { get; set; }

        /// <summary>
        /// Gets or sets how to store the field
        /// </summary>
        public FieldStore Store { get; set; }

        /// <summary>
        /// Gets or sets the type of the analyzer to use for this field
        /// </summary>
        public Type AnalyzerType { get; set; }

        /// <summary>
        /// Gets or sets the boost for this field
        /// </summary>
        public double Boost { get; set; }
    }
}