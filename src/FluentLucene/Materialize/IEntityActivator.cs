using System;

namespace FluentLucene.Materialize
{
    /// <summary>
    /// Represents an activator from which it is possible to create entities
    /// </summary>
    internal interface IEntityActivator
    {
        /// <summary>
        /// Creates an instance of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of the entity to instantiate</typeparam>
        /// <returns>The newly created entity</returns>
        T CreateInstance<T>();

        /// <summary>
        /// Creates an instance of the specified type
        /// </summary>
        /// <param name="type">Type type of the entity to instantiate</param>
        /// <returns>The newly created entity</returns>
        object CreateInstance(Type type);
    }
}