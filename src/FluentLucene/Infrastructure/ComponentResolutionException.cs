using System;
using System.Text;

namespace FluentLucene.Infrastructure
{
    /// <summary>
    /// Represents an exception with component resolution.
    /// </summary>
    [Serializable]
    internal class ComponentResolutionException : ApplicationException
    {
        public ComponentResolutionError Error { get; private set; }
        public ComponentResolutionError RootCause { get; private set; }

        public ComponentResolutionException(string message, ComponentResolutionError error) : base(message)
        {
            Error = RootCause = error;
        }

        public ComponentResolutionException(string message, Exception inner, ComponentResolutionError error) : base(FormatMessage(message, inner), inner)
        {
            Error = error;

            var componentException = inner as ComponentResolutionException;
            
            if (componentException != null)
            {
                RootCause = componentException.RootCause;
            }
        }

        private static string FormatMessage(string message, Exception inner)
        {
            return FormatMessage(message, inner.Message ?? string.Empty);
        }

        public static string FormatMessage(string message, string innerMessage)
        {
            var sb = new StringBuilder();
            sb.AppendLine(message);
            sb.Append((innerMessage ?? string.Empty).Replace(Environment.NewLine, Environment.NewLine + ".. "));
            return sb.ToString();
        }

        public static ComponentResolutionException NotRegistered(Type type)
        {
            throw new ComponentResolutionException(
                    string.Format("Unable to resolve type {0}. The component was not registered.", type),
                    ComponentResolutionError.NotRegistered);
        }

        public static ComponentResolutionException AlreadyRegistered(Type type)
        {
            throw new ComponentResolutionException(
                    string.Format("The type {0} was already registered", type),
                    ComponentResolutionError.AlreadyRegistered);
        }

        public static ComponentResolutionException ConstructorNotFound(Type type)
        {
            throw new ComponentResolutionException(
                    string.Format("Unable to find a suitable constructor for type {0}", type),
                    ComponentResolutionError.ConstructorNotFound);
        }

        public static ComponentResolutionException DependencyResolution(Type type, Exception innerException)
        {
            throw new ComponentResolutionException(
                string.Format("Unable to resolve dependencies for {0}", type),
                innerException,
                ComponentResolutionError.DependencyResolution);
        }

        public static ComponentResolutionException CircularDependency(Type type, Type dependency)
        {
            return new ComponentResolutionException(
                FormatMessage(string.Format("A circular dependency was detected within {0}", type), dependency.ToString()),
                ComponentResolutionError.CircularDependency);
        }
    }
}