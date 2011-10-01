using System;
using FluentLucene.Mapping;
using FluentLucene.Types;

namespace FluentLucene.MappingModel
{
    /// <summary>
    /// Base class for field-like mappings
    /// </summary>
    internal abstract class FieldLikeMappingBase : IFieldLikeMapping
    {
        /// <summary>
        /// The member used to get and set the value of the field
        /// </summary>
        private readonly IMember Member;

        /// <summary>
        /// Creates a field-like mapping
        /// </summary>
        /// <param name="member">The member backing this mapping</param>
        protected FieldLikeMappingBase(IMember member)
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
        /// Gets or sets the mapping type, used to map between .NET types and how they are indexed and stored
        /// </summary>
        public IType MappingType { get; set; }
    }
}