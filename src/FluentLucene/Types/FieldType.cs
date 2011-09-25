using System;
using Lucene.Net.Documents;

namespace FluentLucene.Types
{
    /// <summary>
    /// Defines a mapping between a .NET type and a Lucene <see cref="Field"/>
    /// </summary>
    /// <typeparam name="T">The type of the .NET type mapped</typeparam>
    internal abstract class FieldType<T> : IType
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
            return GetValueInternal((Field)fieldable);
        }

        /// <summary>
        /// Sets the value in the field. This not only possibly sets the stored value but also the
        /// content to be possibly indexed and analyzed.
        /// </summary>
        /// <param name="fieldable">The field in which to set the value</param>
        /// <param name="value">The .NET value to set in the field</param>
        public void SetValue(Fieldable fieldable, object value)
        {
            SetValueInternal((Field) fieldable, (T)value);
        }

        /// <summary>
        /// Gets the implementation of <see cref="Fieldable"/> that this type uses
        /// </summary>
        public Type FieldableType { get { return typeof (Field); } }

        /// <summary>
        /// Typesafe version of <see cref="GetValue"/>
        /// </summary>
        protected abstract T GetValueInternal(Field field);

        /// <summary>
        /// Typesafe version of <see cref="SetValue"/>
        /// </summary>
        protected abstract void SetValueInternal(Field field, T value);
    }
}