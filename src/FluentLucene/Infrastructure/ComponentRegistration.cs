using System;
using System.Diagnostics;

namespace FluentLucene.Infrastructure
{
    /// <summary>
    /// Represents a registration for a component
    /// </summary>
    internal class ComponentRegistration
    {
        /// <summary>
        /// Gets the type of the component registered
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Gets whether the component is a singleton or not
        /// </summary>
        public bool Singleton { get; private set; }

        /// <summary>
        /// Gets the instance
        /// </summary>
        public object Instance { get; private set; }

        /// <summary>
        /// Gets the factory method to use in order to create the instance
        /// </summary>
        public Func<object> FactoryMethod { get; private set; }
        
        /// <summary>
        /// Private constructor.
        /// </summary>
        private ComponentRegistration() { }

        /// <summary>
        /// Creates registration for a singleton instance, created only once using dependency injection
        /// </summary>
        /// <param name="type">The type registered</param>
        /// <returns>The registration</returns>
        public static ComponentRegistration CreateSingleton(Type type)
        {
            return new ComponentRegistration { Type = type, Singleton = true };
        }

        /// <summary>
        /// Creates a registration for a singleton instance, created using the provided factory method
        /// </summary>
        /// <param name="type">The type registered</param>
        /// <param name="factoryMethod">The factory method used to create instances</param>
        /// <returns>The registration</returns>
        public static ComponentRegistration CreateSingleton(Type type, Func<object> factoryMethod)
        {
            return new ComponentRegistration { Type = type, FactoryMethod = factoryMethod, Singleton = true };
        }

        /// <summary>
        /// Creates a registration for transient instances, created every time its needed using dependency injection
        /// </summary>
        /// <param name="type">The type registered</param>
        /// <returns>The registration</returns>
        public static ComponentRegistration CreateTransient(Type type)
        {
            return new ComponentRegistration { Type = type };
        }

        /// <summary>
        /// Creates a registration for transient instances, created using the provided factory method
        /// </summary>
        /// <param name="type">The type registered</param>
        /// <param name="factoryMethod">The factory method used to create instances</param>
        /// <returns>The registration</returns>
        public static ComponentRegistration CreateTransient(Type type, Func<object> factoryMethod)
        {
            return new ComponentRegistration { Type = type, FactoryMethod = factoryMethod };
        }

        /// <summary>
        /// Creates a registration for a instance, provided before-hand
        /// </summary>
        /// <param name="type">The type registered</param>
        /// <param name="instance">The instance</param>
        /// <returns>The registration</returns>
        public static ComponentRegistration CreateInstance(Type type, object instance)
        {
            Debug.Assert(type.IsAssignableFrom(instance.GetType()));
            return new ComponentRegistration { Type = type, Instance = instance };
        }
    }
}