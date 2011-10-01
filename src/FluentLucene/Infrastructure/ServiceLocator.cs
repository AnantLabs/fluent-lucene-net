using System;
using System.Collections;

namespace FluentLucene.Infrastructure
{
    /// <summary>
    /// Implementation of a very simple service locator
    /// </summary>
    internal class ServiceLocator : IServiceLocator
    {
        private readonly ComponentContainer container;

        public ServiceLocator()
        {
            container = new ComponentContainer();
            container.Instance<IServiceLocator, ServiceLocator>(this);
        }

        /// <summary>
        /// Gets the container used to register and get components
        /// </summary>
        public ComponentContainer Container { get { return container; } }

        /// <summary>
        /// Get a component of the given type
        /// </summary>
        /// <param name="parameters">The parameters to inject manually</param>
        /// <typeparam name="T">The type of the component</typeparam>
        /// <returns>The component of the requested type</returns>
        public T Get<T>(Hashtable parameters = null)
        {
            return container.Get<T>(parameters);
        }

        /// <summary>
        /// Get a component of the given type
        /// </summary>
        /// <param name="type">The type of the component</param>
        /// <param name="parameters">The parameters to inject manually</param>
        /// <returns>The component of the requested type</returns>
        public object Get(Type type, Hashtable parameters = null)
        {
            return container.Get(type, parameters);
        }
    }
}