using System;
using System.Reflection;

namespace FluentLucene.Mapping
{
    /// <summary>
    /// Represents a member backed by a property
    /// </summary>
    internal class FieldMember : MemberBase, IEquatable<FieldMember>
    {
        /// <summary>
        /// The property info backing this member
        /// </summary>
        private readonly FieldInfo Field;

        /// <summary>
        /// Creates a field member backed by the specified field
        /// </summary>
        /// <param name="field">The field</param>
        public FieldMember(FieldInfo field) : base(field)
        {
            Field = field;
        }

        /// <summary>
        /// Gets the value of the member for a given target
        /// </summary>
        /// <param name="target">The target from which to gather the value</param>
        /// <returns>The value of the target for that member</returns>
        public override object GetValue(object target)
        {
            return Field.GetValue(target);
        }

        /// <summary>
        /// Sets the value of the member for a given target
        /// </summary>
        /// <param name="target">The target for which to set the value</param>
        /// <param name="value">The value to set for the member</param>
        public override void SetValue(object target, object value)
        {
            Field.SetValue(target, value);
        }

        /// <summary>
        /// Gets the type of the value for this member
        /// </summary>
        public override Type MemberType { get { return Field.FieldType; } }

        /// <summary>
        /// Gets the name of the member
        /// </summary>
        public override string Name { get { return Field.Name; } }

        public bool Equals(FieldMember other)
        {
            return base.Equals(other);
        }
    }
}