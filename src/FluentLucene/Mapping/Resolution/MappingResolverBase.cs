using System.Linq;

namespace FluentLucene.Mapping.Resolution
{
    /// <summary>
    /// Base implementation of a mapping resolver
    /// </summary>
    internal abstract class MappingResolverBase : IMappingResolver
    {
        /// <summary>
        /// Returns whether or not the resolver has recommendations
        /// </summary>
        /// <param name="container">The container with the mappings</param>
        /// <returns>true if the resolve has recommendations to make; false otherwise</returns>
        public bool HasRecommendations(IMappingContainer container)
        {
            return GetRecommendation(container).HasRecommendations;
        }

        /// <summary>
        /// Returns whether or not the resolve has recommendations and can resolve some of them
        /// </summary>
        /// <param name="container">The container with the mappings</param>
        /// <returns>true if the resolve can resolve at least one recommendation; false otherwise</returns>
        public bool HasResolutions(IMappingContainer container)
        {
            return GetRecommendation(container).Recommendations.Any(x => x.CanResolve);
        }

        /// <summary>
        /// Resolves any resolvable recommendation for the specified mappings
        /// </summary>
        /// <param name="container">The container with the mappings</param>
        /// <returns>true if any resolution was applied to the mappings; false otherwise</returns>
        public abstract bool Resolve(IMappingContainer container);

        /// <summary>
        /// Gets the recommendation of any improvement or resolution that could be applied to the mappings. The method
        /// never returns null.
        /// </summary>
        /// <param name="container">The container with the mappings</param>
        /// <returns>The recommendation for the mappings; this method never returns null</returns>
        public abstract ResolutionRecommendation GetRecommendation(IMappingContainer container);
    }
}