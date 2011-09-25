using System;
using Lucene.Net.Documents;

namespace FluentLucene.Types
{
    /// <summary>
    /// Defines a mapping between a .NET type and a Lucene <see cref="NumericField"/>
    /// </summary>
    /// <typeparam name="T">The type of the .NET type mapped</typeparam>
    internal abstract class NumericFieldType<T> : IType
    {
        /// <summary>
        /// Gets the value stored in the fieldable. 
        /// </summary>
        /// <remarks>
        /// This method is never called if the value is not stored in the index.
        /// </remarks>
        /// <param name="fieldable">The fieldable containaing the value.</param>
        /// <returns>The .NET value that was stored in the field</returns>
        public object GetValue(Fieldable fieldable)
        {
            if (string.IsNullOrEmpty(fieldable.StringValue())) return null;
            return GetValueInternal((NumericField)fieldable);
        }

        /// <summary>
        /// Sets the value in the field. This not only possibly sets the stored value but also the
        /// content to be possibly indexed and analyzed.
        /// </summary>
        /// <param name="fieldable">The field in which to set the value</param>
        /// <param name="value">The .NET value to set in the field</param>
        /// <returns>true if the value was set; false if the value was not set and the field should be ignored (not added to a document)</returns>
        public bool SetValue(Fieldable fieldable, object value)
        {
            if (value == null) return false;
            SetValueInternal((NumericField)fieldable, (T)value);
            return true;
        }

        /// <summary>
        /// Gets the implementation of <see cref="Fieldable"/> that this type uses
        /// </summary>
        public Type FieldableType { get { return typeof(NumericField); } }

        /// <summary>
        /// Gets whether or not values set using this type are lexicographically ordered. That means, whether or
        /// not this type produces fields that are sutable for sorting and for range queries.
        /// </summary>
        public abstract bool IsLexicographicallyOrdered { get; }

        /// <summary>
        /// Typesafe version of <see cref="GetValue"/>
        /// </summary>
        protected abstract T GetValueInternal(NumericField field);

        /// <summary>
        /// Typesafe version of <see cref="SetValue"/>
        /// </summary>
        protected abstract void SetValueInternal(NumericField field, T value);
    }
}