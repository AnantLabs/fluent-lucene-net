using System;
using System.Text;

namespace FluentLucene.Infrastructure
{
    internal enum ComponentResolutionError
    {
        None = 0,
        AlreadyRegistered,
        NotRegistered,
        ConstructorNotFound,
        DependencyResolution,
        CircularDependency
    }

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
    }
}