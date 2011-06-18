using System;
using FluentLucene.Mapping;

namespace FluentLucene.MappingModel
{
    /// <summary>
    /// The mapping repesenting a field in a document
    /// </summary>
    internal class FieldMapping : IFieldLikeMapping
    {
        /// <summary>
        /// The member used to get and set the value of the field
        /// </summary>
        private readonly IMember Member;

        /// <summary>
        /// Creates a field mapping backed by the specified member
        /// </summary>
        /// <param name="member">The member backing this field</param>
        public FieldMapping(IMember member)
        {
            // Set the member
            Member = member;

            // Set the default values according to the member
            Name = Member.Name;
        }

        /// <summary>
        /// Gets the value of the field for a given target
        /// </summary>
        /// <param name="target">The target from which to gather the value</param>
        /// <returns>The value of the target for that member</returns>
        public object GetValue(object target)
        {
            return Member.GetValue(target);
        }

        /// <summary>
        /// Sets the value of the member for a given target
        /// </summary>
        /// <param name="target">The target for which to set the value</param>
        /// <param name="value">The value to set for the member</param>
        public void SetValue(object target, object value)
        {
            Member.SetValue(target, value);
        }

        /// <summary>
        /// Gets the type of the value for this member
        /// </summary>
        public Type Type { get { return Member.MemberType; } }

        /// <summary>
        /// Gets the name of the member
        /// </summary>
        public string Name { get; set; }

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