namespace FluentLucene.Mapping.Resolution
{
    /// <summary>
    /// Represents a single resolution description
    /// </summary>
    public class ResolutionDescription
    {
        /// <summary>
        /// Creates a resolution
        /// </summary>
        /// <param name="type">The type of the resolution</param>
        /// <param name="description">The human-readable description of the resolution</param>
        /// <param name="canResolve">Whether or not the resolution can be resolved</param>
        public ResolutionDescription(ResolutionType type, string description, bool canResolve)
        {
            Type = type;
            Description = description;
            CanResolve = canResolve;
        }

        /// <summary>
        /// Gets the type of the resolution
        /// </summary>
        public ResolutionType Type { get; private set; }

        /// <summary>
        /// Gets the human-readable description of the resolution
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets whether or not the resolution can be resolved
        /// </summary>
        public bool CanResolve { get; private set; }
    }
}