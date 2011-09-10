using System;

namespace FluentLucene.Infrastructure
{
    /// <summary>
    /// Represents a simple service locator
    /// </summary>
    internal interface IServiceLocator
    {
        /// <summary>
        /// Get a component of the given type
        /// </summary>
        /// <typeparam name="T">The type of the component</typeparam>
        /// <returns>The component of the requested type</returns>
        T Get<T>();

        /// <summary>
        /// Get a component of the given type
        /// </summary>
        /// <param name="type">The type of the component</param>
        /// <returns>The component of the requested type</returns>
        object Get(Type type);
    }
}