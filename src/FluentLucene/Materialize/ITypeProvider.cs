using System;
using FluentLucene.Types;

namespace FluentLucene.Materialize
{
    /// <summary>
    /// Represents a provider for mapping types
    /// </summary>
    internal interface ITypeProvider
    {
        /// <summary>
        /// Gets a mapping for the given .NET type
        /// </summary>
        /// <param name="type">The .NET type</param>
        /// <returns>The mapping type, or null</returns>
        IType GetFor(Type type);
    }
}