namespace FluentLucene.Search
{
    public interface ISearchOverBetween<TRoot, TSubType>
    {
        /// <summary>
        /// Upper bound of the range search.
        /// </summary>
        /// <param name="upperBound">Upper bound (single word only).</param>
        ISearchOver<TRoot, TSubType> And(string upperBound);
    }
}