using System;
using System.Reflection;

namespace FluentLucene.Mapping
{
    /// <summary>
    /// Represents a member backed by a property
    /// </summary>
    internal class PropertyMember : MemberBase, IEquatable<PropertyMember>
    {
        /// <summary>
        /// The property info backing this member
        /// </summary>
        private readonly PropertyInfo Property;

        /// <summary>
        /// Creates a property member backed by the specified property
        /// </summary>
        /// <param name="property">The property</param>
        public PropertyMember(PropertyInfo property) : base(property)
        {
            Property = property;
        }

        /// <summary>
        /// Gets the value of the member for a given target
        /// </summary>
        /// <param name="target">The target from which to gather the value</param>
        /// <returns>The value of the target for that member</returns>
        public override object GetValue(object target)
        {
            return Property.GetValue(target, null);
        }

        /// <summary>
        /// Sets the value of the member for a given target
        /// </summary>
        /// <param name="target">The target for which to set the value</param>
        /// <param name="value">The value to set for the member</param>
        public override void SetValue(object target, object value)
        {
            Property.SetValue(target, value, null);
        }

        /// <summary>
        /// Gets the type of the value for this member
        /// </summary>
        public override Type MemberType { get { return Property.PropertyType; } }

        /// <summary>
        /// Gets the name of the member
        /// </summary>
        public override string Name { get { return Property.Name; } }

        public bool Equals(PropertyMember other)
        {
            return base.Equals(other);
        }
    }
}