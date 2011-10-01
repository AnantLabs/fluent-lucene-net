using System;
using FluentLucene.Types;

namespace FluentLucene.MappingModel
{
    /// <summary>
    /// Represents a field-like mapping
    /// </summary>
    internal interface IFieldLikeMapping
    {
        /// <summary>
        /// Gets the value of the member for a given target
        /// </summary>
        /// <param name="target">The target from which to gather the value</param>
        /// <returns>The value of the target for that member</returns>
        object GetValue(object target);

        /// <summary>
        /// Sets the value of the member for a given target
        /// </summary>
        /// <param name="target">The target for which to set the value</param>
        /// <param name="value">The value to set for the member</param>
        void SetValue(object target, object value);

        /// <summary>
        /// Gets the type of the value for this field
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Gets the name of the field
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the mapping type, used to map between .NET types and how they are indexed and stored
        /// </summary>
        IType MappingType { get; }
    }
}