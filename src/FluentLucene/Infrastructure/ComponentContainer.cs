using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentLucene.Infrastructure
{
    /// <summary>
    /// Container for components. The container is not a performance superstar, but performs pretty well with
    /// singleton components.
    /// </summary>
    /// <remarks>
    /// This container is implemented from scratch to avoid having external dependencies to IoC containers like Castle.Windsor.
    /// </remarks>
    internal class ComponentContainer
    {
        /// <summary>
        /// All of the registered components
        /// </summary>
        private readonly Dictionary<Type, ComponentRegistration> Registrations = new Dictionary<Type, ComponentRegistration>();

        /// <summary>
        /// All of the registered and created singletons
        /// </summary>
        private readonly Dictionary<Type, object> Singletons = new Dictionary<Type, object>();
        
        /// <summary>
        /// Registers a singleton component, created at most once, when requested.
        /// </summary>
        /// <typeparam name="T">The type of the component</typeparam>
        public void Singleton<T>()
        {
            Singleton<T, T>();
        }

        /// <summary>
        /// Registers a singleton component, created at most once, when requested.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation</typeparam>
        public void Singleton<TInterface, TImplementation>() where TImplementation : TInterface
        {
            // Ensure the type was not already registered
            EnsureNotRegistered(typeof (TInterface));

            // Add a registration for that type
            Registrations.Add(typeof(TInterface), new ComponentRegistration(typeof(TImplementation), ComponentLifetime.Singleton));
        }

        /// <summary>
        /// Registers a transient component, to be created each time it is requested
        /// </summary>
        /// <typeparam name="T">The type of the component</typeparam>
        public void Transient<T>()
        {
            Transient<T, T>();
        }

        /// <summary>
        /// Registers a transient component, to be created each time it is requested
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation</typeparam>
        public void Transient<TInterface, TImplementation>() where TImplementation : TInterface
        {
            // Ensure the type was not already registered
            EnsureNotRegistered(typeof(TInterface));

            // Add a registration for that type
            Registrations.Add(typeof(TInterface), new ComponentRegistration(typeof(TImplementation), ComponentLifetime.Transient));
        }

        /// <summary>
        /// Ensure no component of the given type is registered yet
        /// </summary>
        /// <param name="type">The type of the component</param>
        private void EnsureNotRegistered(Type type)
        {
            // Ensure the type was not already registered
            if (Registrations.ContainsKey(type))
            {
                throw new ComponentResolutionException(
                    string.Format("The type {0} was already registered", type),
                    ComponentResolutionError.AlreadyRegistered);
            }
        }

        /// <summary>
        /// Get a component of the given type
        /// </summary>
        /// <typeparam name="T">The type of the component</typeparam>
        /// <returns>The component of the requested type</returns>
        public T Get<T>()
        {
            return (T) GetInternal(typeof (T), null);
        }

        /// <summary>
        /// Get a component of the given type
        /// </summary>
        /// <param name="type">The type of the component</param>
        /// <returns>The component of the requested type</returns>
        public object Get(Type type)
        {
            return GetInternal(type, null);
        }

        /// <summary>
        /// Internal method for getting a type by specifying and existing set of pending resolutions, mainly used
        /// to detect circular dependencies
        /// </summary>
        /// <param name="type">The type of the component</param>
        /// <param name="pendingResolutions">A set of pending resolutions</param>
        /// <returns></returns>
        private object GetInternal(Type type, IDictionary<Type, ComponentRegistration> pendingResolutions)
        {
            // Get the registration for the given type
            ComponentRegistration registration;

            if (!Registrations.TryGetValue(type, out registration))
            {
                throw new ComponentResolutionException(
                    string.Format("Unable to resolve type {0}. The component was not registered.", type),
                    ComponentResolutionError.NotRegistered);
            }
            // Try and retrieve the instance as a singleton instance when appropriate
            if (registration.Lifetime == ComponentLifetime.Singleton)
            {
                object singleton;

                if (Singletons.TryGetValue(type, out singleton)) 
                    return singleton;
            }

            // Find the constructor for the given type
            var ctor = GetConstructor(registration.Type);

            // Ensure there is an eligible constructor
            if (ctor == null)
            {
                throw new ComponentResolutionException(
                    string.Format("Unable to find a suitable constructor for type {0}", type),
                    ComponentResolutionError.ConstructorNotFound);
            }

            // Resolve all of the dependencies
            var dependencies = ResolveDependencies(ctor).ToArray();

            // Create a dictionary of seen type that are not yet resolved
            pendingResolutions = pendingResolutions ?? new Dictionary<Type, ComponentRegistration>();

            // Add an entry for the current type in the pending resolutions
            pendingResolutions.Add(type, registration);

            // The list of resolved dependencies
            var resolvedDependencies = new object[dependencies.Length];
            var currentDependencyIndex = 0;

            // For every dependency
            foreach (var dependency in dependencies)
            {
                // Ensure there is no circular dependency
                if (pendingResolutions.ContainsKey(dependency))
                {
                    throw new ComponentResolutionException(
                        ComponentResolutionException.FormatMessage(
                            string.Format("A circular dependency was detected within {0}", type),
                            dependency.ToString()),
                        ComponentResolutionError.CircularDependency);
                }

                try
                {
                    // Create an instance of the dependency recursively
                    resolvedDependencies[currentDependencyIndex++] = GetInternal(dependency, pendingResolutions);
                }
                catch (ComponentResolutionException ex)
                {
                    // Wrap an throw the exception
                    throw new ComponentResolutionException(
                        string.Format("Unable to resolve dependencies for {0}", type), 
                        ex,
                        ComponentResolutionError.DependencyResolution);
                }
            }

            // Create an instance of the type using the constructor obtained before
            var instance = ctor.Invoke(resolvedDependencies);

            // Register the instance as a singleton when appropriate
            if (registration.Lifetime == ComponentLifetime.Singleton)
                Singletons.Add(type, instance);

            // Return the newly created instance
            return instance;
        }

        /// <summary>
        /// Gets the constructor to use for insantiation
        /// </summary>
        /// <param name="type">The type for which to obtain a constructor</param>
        /// <returns>The constructor to use for instantiation or null if no candidates exist</returns>
        private static ConstructorInfo GetConstructor(Type type)
        {
            ConstructorInfo ctor = null;
            int nbParams = -1;

            // Find the constructor with the most parameters
            foreach (var constructor in type.GetConstructors())
            {
                var nbParamsCurrent = constructor.GetParameters().Length;

                if (nbParamsCurrent > nbParams)
                {
                    ctor = constructor;
                    nbParams = nbParamsCurrent;
                }
            }

            return ctor;
        }

        /// <summary>
        /// Resolve the dependencies of a given type, for a given constructor
        /// </summary>
        /// <param name="ctor">The constuctor info</param>
        /// <returns>A sequence of dependencies for that type</returns>
        private static IEnumerable<Type> ResolveDependencies(ConstructorInfo ctor)
        {
            return ctor.GetParameters().Select(x => x.ParameterType);
        }
    }
}