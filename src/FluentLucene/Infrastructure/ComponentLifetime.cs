namespace FluentLucene.Infrastructure
{
    /// <summary>
    /// Represents the lifetime of the component
    /// </summary>
    internal enum ComponentLifetime
    {
        /// <summary>
        /// A transient component, returning a new instance each time
        /// </summary>
        Transient = 0,

        /// <summary>
        /// A singleton component, created only once
        /// </summary>
        Singleton = 1
    }
}