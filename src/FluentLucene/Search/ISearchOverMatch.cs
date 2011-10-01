namespace FluentLucene.Search
{
    public interface ISearchOverMatch<TRoot, TSubType> : ISearchOver<TRoot, TSubType>
    {
        /// <summary>
        /// Indicates that the match is required.
        /// </summary>
        ISearchOverMatch<TRoot, TSubType> Required();

        /// <summary>
        /// Indicates that the relevance level of the match is boosted.
        /// The higher the boost factor, the more relevant the term will be.
        /// </summary>
        /// <param name="boost">Boost factor, must be positive.</param>
        ISearchOverMatch<TRoot, TSubType> Boost(double boost);
    }
}