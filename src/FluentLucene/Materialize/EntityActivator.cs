using System;

namespace FluentLucene.Materialize
{
    /// <summary>
    /// Implementation of an activator used to create entities.
    /// </summary>
    internal class EntityActivator
    {
        /// <summary>
        /// Creates an instance of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of the entity to instantiate</typeparam>
        /// <returns>The newly created entity</returns>
        public T CreateInstance<T>()
        {
            return (T)Activator.CreateInstance(typeof (T));
        }

        /// <summary>
        /// Creates an instance of the specified type
        /// </summary>
        /// <param name="type">Type type of the entity to instantiate</param>
        /// <returns>The newly created entity</returns>
        public object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}