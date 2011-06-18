using System;
using FluentLucene.Mapping;

namespace FluentLucene.MappingModel
{
    /// <summary>
    /// The mapping representing the identity of a document
    /// </summary>
    internal class IdentityMapping : IFieldLikeMapping
    {
        /// <summary>
        /// The member used to get and set the value of the field
        /// </summary>
        private readonly IMember Member;

        /// <summary>
        /// Creates an identity mapping backed by the specified member
        /// </summary>
        /// <param name="member">The member backing this identity</param>
        public IdentityMapping(IMember member)
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
    }
}