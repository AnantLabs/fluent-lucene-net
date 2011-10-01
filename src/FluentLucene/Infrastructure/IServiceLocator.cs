using System;
using System.Collections;

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
        /// <param name="parameters">The parameters to inject manually</param>
        /// <typeparam name="T">The type of the component</typeparam>
        /// <returns>The component of the requested type</returns>
        T Get<T>(Hashtable parameters = null);

        /// <summary>
        /// Get a component of the given type
        /// </summary>
        /// <param name="type">The type of the component</param>
        /// <param name="parameters">The parameters to inject manually</param>
        /// <returns>The component of the requested type</returns>
        object Get(Type type, Hashtable parameters = null);

        /// <summary>
        /// Gets the container used to register and get components
        /// </summary>
        ComponentContainer Container { get; }
    }
}