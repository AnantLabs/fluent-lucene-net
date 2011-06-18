using System;

namespace FluentLucene.Infrastructure
{
    internal class ComponentRegistration
    {
        /// <summary>
        /// Gets the type of the component registered
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets the lifetime of the component registered
        /// </summary>
        public ComponentLifetime Lifetime { get; set; }

        /// <summary>
        /// Creates a registration for a given type
        /// </summary>
        /// <param name="type">The type of the component to register</param>
        /// <param name="lifetime">The lifetime of the component</param>
        public ComponentRegistration(Type type, ComponentLifetime lifetime)
        {
            Type = type;
            Lifetime = lifetime;
        }
    }
}