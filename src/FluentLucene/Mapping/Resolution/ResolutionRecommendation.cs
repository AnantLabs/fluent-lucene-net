using System.Collections.Generic;
using System.Linq;

namespace FluentLucene.Mapping.Resolution
{
    /// <summary>
    /// Represents the description of an entire resolution recommendation
    /// </summary>
    public class ResolutionRecommendation
    {
        /// <summary>
        /// Creates a resolution recommendation
        /// </summary>
        /// <param name="recommendations">All of the recommendation</param>
        public ResolutionRecommendation(IEnumerable<ResolutionDescription> recommendations)
        {
            this.recommendations = recommendations.ToList();
        }

        /// <summary>
        /// Whether there is any recommendation or not
        /// </summary>
        public bool HasRecommendations { get { return recommendations.Count > 0; } }

        /// <summary>
        /// Backing field for all of the recommendations
        /// </summary>
        private readonly List<ResolutionDescription> recommendations;

        /// <summary>
        /// Gets all of the recommendations
        /// </summary>
        public IEnumerable<ResolutionDescription> Recommendations { get { return recommendations; } }

        #region Factory methods

        /// <summary>
        /// Gets an empty resolution recommendation
        /// </summary>
        public static ResolutionRecommendation Empty { get { return new ResolutionRecommendation(Enumerable.Empty<ResolutionDescription>()); } }

        /// <summary>
        /// Aggregates recommendations into a single recommendation
        /// </summary>
        /// <param name="recommendations">The recommendations to aggregate</param>
        /// <returns>A single recommendation aggregates all of the specified recommendations</returns>
        public static ResolutionRecommendation Aggregate(IEnumerable<ResolutionRecommendation> recommendations)
        {
            return new ResolutionRecommendation(recommendations.SelectMany(x => x.Recommendations));
        }

        #endregion
    }
}