using System;
using System.Reflection;

namespace FluentLucene.Mapping
{
    /// <summary>
    /// Represents an abstract member that implements basic functionnality
    /// </summary>
    internal abstract class MemberBase : IMember
    {
        /// <summary>
        /// The member backing this instance
        /// </summary>
        private readonly MemberInfo Member;

        /// <summary>
        /// Creates a member from a member info backing it
        /// </summary>
        /// <param name="member">The member info</param>
        protected MemberBase(MemberInfo member)
        {
            Member = member;
        }

        /// <summary>
        /// Gets the value of the member for a given target
        /// </summary>
        /// <param name="target">The target from which to gather the value</param>
        /// <returns>The value of the target for that member</returns>
        public abstract object GetValue(object target);

        /// <summary>
        /// Sets the value of the member for a given target
        /// </summary>
        /// <param name="target">The target for which to set the value</param>
        /// <param name="value">The value to set for the member</param>
        public abstract void SetValue(object target, object value);

        /// <summary>
        /// Gets the type of the value for this member
        /// </summary>
        public abstract Type MemberType { get; }

        /// <summary>
        /// Gets the name of the member
        /// </summary>
        public abstract string Name { get; }

        #region Equals

        public override bool Equals(object obj)
        {
            var other = obj as MemberBase;
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(other.Member.MetadataToken, Member.MetadataToken) && Equals(other.Member.Module, Member.Module);
        }

        public override int GetHashCode()
        {
            return Member.GetHashCode();
        }

        #endregion
    }
}