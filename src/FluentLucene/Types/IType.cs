using System;
using Lucene.Net.Documents;

namespace FluentLucene.Types
{
    /// <summary>
    /// Defines a mapping between a .NET type and Lucene
    /// </summary>
    /// <remarks>
    /// <para>
    /// Implementors should not check for or enforce correct formatting or type (read: Parse, not TryParse.)
    /// Wrapping exceptions to provided a more readable error message is generally preferred.
    /// </para>
    /// <para>
    /// <see cref="GetValue"/> and <see cref="SetValue"/> can safely throw any type of exception.
    /// </para>
    /// </remarks>
    internal interface IType
    {
        /// <summary>
        /// Gets the value stored in the fieldable. 
        /// </summary>
        /// <remarks>
        /// This method is never called if the value is not stored in the index.
        /// </remarks>
        /// <param name="fieldable">The fieldable containaing the value.</param>
        /// <returns>The .NET value that was stored in the field</returns>
        object GetValue(Fieldable fieldable);

        /// <summary>
        /// Sets the value in the field. This not only possibly sets the stored value but also the
        /// content to be possibly indexed and analyzed.
        /// </summary>
        /// <param name="fieldable">The field in which to set the value</param>
        /// <param name="value">The .NET value to set in the field</param>
        void SetValue(Fieldable fieldable, object value);

        /// <summary>
        /// Gets the implementation of <see cref="Fieldable"/> that this type uses
        /// </summary>
        Type FieldableType { get; }
    }
}