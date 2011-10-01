using System;
using System.Collections;
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
        /// <param name="parameters">The parameters to inject manually</param>
        /// <typeparam name="T">The type of the component</typeparam>
        public void Singleton<T>(Hashtable parameters = null)
        {
            Singleton<T, T>(parameters);
        }

        /// <summary>
        /// Registers a singleton component, created at most once, when requested.
        /// </summary>
        /// <param name="parameters">The parameters to inject manually</param>
        /// <typeparam name="TInterface">The type of the interface</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation</typeparam>
        public void Singleton<TInterface, TImplementation>(Hashtable parameters = null) where TImplementation : TInterface
        {
            // Ensure the type was not already registered
            EnsureNotRegistered(typeof (TInterface));

            // Add a registration for that type
            Registrations.Add(typeof (TInterface), ComponentRegistration.CreateSingleton(typeof (TImplementation), parameters));
        }

        /// <summary>
        /// Registers a singleton component, created at most once when requested, using the provided factory method
        /// </summary>
        /// <param name="factoryMethod">The factory method to use in order to create the component</param>
        /// <param name="parameters">The parameters to inject manually</param>
        /// <typeparam name="T">The type of the component</typeparam>
        public void Singleton<T>(Func<T> factoryMethod, Hashtable parameters = null)
        {
            // Ensure the type was not already registered
            EnsureNotRegistered(typeof(T));

            // Add a registration for that type
            Registrations.Add(typeof(T), ComponentRegistration.CreateSingleton(typeof(T), () => factoryMethod(), parameters));
        }

        /// <summary>
        /// Registers a transient component, to be created each time it is requested
        /// </summary>
        /// <param name="parameters">The parameters to inject manually</param>
        /// <typeparam name="T">The type of the component</typeparam>
        public void Transient<T>(Hashtable parameters = null)
        {
            Transient<T, T>(parameters);
        }

        /// <summary>
        /// Registers a transient component, to be created each time it is requested
        /// </summary>
        /// <param name="parameters">The parameters to inject manually</param>
        /// <typeparam name="TInterface">The type of the interface</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation</typeparam>
        public void Transient<TInterface, TImplementation>(Hashtable parameters = null) where TImplementation : TInterface
        {
            // Ensure the type was not already registered
            EnsureNotRegistered(typeof(TInterface));

            // Add a registration for that type
            Registrations.Add(typeof(TInterface), ComponentRegistration.CreateTransient(typeof(TImplementation), parameters));
        }

        /// <summary>
        /// Registers a transient component, to be created each time it is requested using the provided factory method
        /// </summary>
        /// <param name="factoryMethod">The factory method to use in order to create the component</param>
        /// <param name="parameters">The parameters to inject manually</param>
        /// <typeparam name="T">The type of the component</typeparam>
        public void Transient<T>(Func<T> factoryMethod, Hashtable parameters = null)
        {
            // Ensure the type was not already registered
            EnsureNotRegistered(typeof(T));

            // Add a registration for that type
            Registrations.Add(typeof(T), ComponentRegistration.CreateTransient(typeof(T), () => factoryMethod(), parameters));
        }

        /// <summary>
        /// Registers a component by specifying the instance to use
        /// </summary>
        /// <param name="instance">The instance to user</param>
        /// <typeparam name="T">The instance of the component</typeparam>
        public void Instance<T>(T instance)
        {
            Instance<T, T>(instance);
        }

        /// <summary>
        /// Registers a component by specifying the instance to use
        /// </summary>
        /// <param name="instance">The instance to user</param>
        /// <typeparam name="TInterface">The type of the interface</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation</typeparam>
        public void Instance<TInterface, TImplementation>(TInterface instance) where TImplementation : TInterface
        {
            // Ensure the type was not already registered
            EnsureNotRegistered(typeof(TInterface));

            // Add a registration for that type
            Registrations.Add(typeof(TInterface), ComponentRegistration.CreateInstance(typeof(TImplementation), instance));
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
                throw ComponentResolutionException.AlreadyRegistered(type);
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

            // Ensure the component is registered
            if (!Registrations.TryGetValue(type, out registration))
            {
                throw ComponentResolutionException.NotRegistered(type);
            }

            // Try and retrieve the instance as a singleton instance when appropriate
            if (registration.Singleton)
            {
                object singleton;

                if (Singletons.TryGetValue(type, out singleton)) 
                    return singleton;
            }

            object instance;

            if (registration.Instance != null)
            {
                // Use the instance if provided
                instance = registration.Instance;
            }
            else if (registration.FactoryMethod != null)
            {
                // Use the factory method provided
                instance = registration.FactoryMethod();
            }
            else
            {
                // Find the constructor for the given type
                var ctor = GetConstructor(registration.Type);

                // Ensure there is an eligible constructor
                if (ctor == null)
                {
                    throw ComponentResolutionException.ConstructorNotFound(type);
                }

                var dependencies = ctor.GetParameters().ToList();

                // Create a dictionary of seen type that are not yet resolved
                pendingResolutions = pendingResolutions ?? new Dictionary<Type, ComponentRegistration>();

                // Add an entry for the current type in the pending resolutions
                pendingResolutions.Add(type, registration);

                // The list of resolved dependencies
                var resolvedDependencies = new object[dependencies.Count];
                var currentDependencyIndex = 0;

                // For every dependency
                foreach (var dependency in dependencies)
                {
                    // Try and get the parameter from the manual parameter list
                    var argument = registration.Parameters[dependency.Name];
                    if (argument != null && dependency.ParameterType.IsAssignableFrom(argument.GetType()))
                    {
                        resolvedDependencies[currentDependencyIndex++] = argument;
                    }
                    else
                    {
                        // The dependency could not be resolved with manually provided parameters.
                        // Resolve it using other components registered

                        var dependencyType = dependency.ParameterType;

                        // Ensure there is no circular dependency
                        if (pendingResolutions.ContainsKey(dependencyType))
                        {
                            throw ComponentResolutionException.CircularDependency(type, dependencyType);
                        }

                        try
                        {
                            // Create an instance of the dependency recursively
                            resolvedDependencies[currentDependencyIndex++] = GetInternal(dependencyType, pendingResolutions);
                        }
                        catch (ComponentResolutionException ex)
                        {
                            // Wrap an throw the exception
                            throw ComponentResolutionException.DependencyResolution(type, ex);
                        }
                    }
                }

                // Create an instance of the type using the constructor obtained before
                instance = ctor.Invoke(resolvedDependencies);
            }

            // Register the instance as a singleton when appropriate
            if (registration.Singleton)
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
    }
}