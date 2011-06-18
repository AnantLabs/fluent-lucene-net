using System.Collections.Generic;
using FluentLucene.MappingModel;

namespace FluentLucene.Mapping.Resolution
{
    /// <summary>
    /// Simple mapping resolver for mapping-intrinsic resolutions
    /// </summary>
    internal abstract class SimpleMappingResolverBase : MappingResolverBase
    {
        /// <summary>
        /// Resolves any resolvable recommendation for the specified mappings
        /// </summary>
        /// <param name="container">The container with the mappings</param>
        /// <returns>true if any resolution was applied to the mappings; false otherwise</returns>
        public override bool Resolve(IMappingContainer container)
        {
            bool resolved = false;

            // For every mapping in the container
            foreach (var mapping in container.Mappings)
            {
                // Try and resolve the mappings
                resolved = resolved || Resolve(mapping);
            }

            // Return whether resolutions were applied or not
            return resolved;
        }

        /// <summary>
        /// Resolves any resolvable recommendation for the specified mappings
        /// </summary>
        /// <param name="mapping">The mappings</param>
        /// <returns>true if any resolution was applied to the mappings; false otherwise</returns>
        protected abstract bool Resolve(DocumentMapping mapping);

        /// <summary>
        /// Gets the recommendation of any improvement or resolution that could be applied to the mappings. The method
        /// never returns null.
        /// </summary>
        /// <param name="container">The container with the mappings</param>
        /// <returns>The recommendation for the mappings; this method never returns null</returns>
        public override ResolutionRecommendation GetRecommendation(IMappingContainer container)
        {
            var recommendations = new List<ResolutionRecommendation>();

            // For every mapping in the container
            foreach (var mapping in container.Mappings)
            {
                // Get the recommendations for the mapping
                recommendations.Add(GetRecommendation(mapping));
            }

            // Return a single recommendation
            return ResolutionRecommendation.Aggregate(recommendations);
        }

        /// <summary>
        /// Gets the recommendation of any improvement or resolution that could be applied to the mappings. The method
        /// never returns null.
        /// </summary>
        /// <param name="mapping">The mappings</param>
        /// <returns>The recommendation for the mappings; this method never returns null</returns>
        public abstract ResolutionRecommendation GetRecommendation(DocumentMapping mapping);
    }
}