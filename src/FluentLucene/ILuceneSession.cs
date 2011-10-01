using FluentLucene.Search;

namespace FluentLucene
{
    /// <summary>
    /// The main interface for FluentLucene
    /// </summary>
    public interface ILuceneSession
    {
        /// <summary>
        /// Creates a new <see cref="ISearchOver{T}"/> for the entity class.
        /// </summary>
        /// <typeparam name="T">The entity class</typeparam>
        /// <returns>An <see cref="ISearchOver{T}"/> object</returns>
        ISearchOver<T, T> SearchOver<T>() where T : class;
    }

    public interface ISearchOverField<TRoot, TSubType>
    {
        /// <summary>
        /// Reverses the condition
        /// </summary>
        ISearchOverField<TRoot, TSubType> Not { get; }

        /// <summary>
        /// Perform a search on the field using Lucene native syntax.
        /// </summary>
        /// <param name="nativeSearchString">Search term or phrase using Lucene native syntax</param>
        ISearchOver<TRoot, TSubType> MatchesNative(string nativeSearchString);

        /// <summary>
        /// Perform a search on the field.
        /// </summary>
        /// <param name="searchString">Search term or phrase</param>
        ISearchOverMatch<TRoot, TSubType> Matches(string searchString);

        /// <summary>
        /// Perform a search on the field.
        /// </summary>
        /// <param name="term">Search term (single word only)</param>
        ISearchOver<TRoot, TSubType> StartsWith(string term);

        /// <summary>
        /// Performs a fuzzy search on the field. Will match words similar in spelling to the term specified.
        /// 
        /// The default required similarity that is used is 0.5.
        /// </summary>
        /// <param name="term">Search term (single word only)</param>
        ISearchOver<TRoot, TSubType> IsLike(string term);

        /// <summary>
        /// Performs a fuzzy search on the field. Will match words similar in spelling to the term specified.
        /// </summary>
        /// <param name="term">Search term (single word only)</param>
        /// <param name="similarity">Required similarity. The value is between 0 and 1, with a value closer to 1 only terms with a higher similarity will be matched.</param>
        ISearchOver<TRoot, TSubType> IsLike(string term, double similarity);

        /// <summary>
        /// Performs a proximity search on the field. Will find words that are a within a specific distance away.
        /// </summary>
        /// <param name="first">First search term (single word only)</param>
        /// <param name="second">Second search term (single word only)</param>
        /// <param name="maxDistance">Maximum number of words between each other.</param>
        ISearchOver<TRoot, TSubType> AreNear(string first, string second, int maxDistance);

        /// <summary>
        /// Identical semantics to IsInclusivelyBetween() to allow more readable queries.
        /// </summary>
        /// <param name="lowerBound">Lower bound (single word only).</param>
        ISearchOverBetween<TRoot, TSubType> IsBetween(string lowerBound);

        /// <summary>
        /// Performs an inclusive range search to match field values that are between the lower and upper bound specified.
        /// Sorting is done lexicographically.
        /// </summary>
        /// <param name="lowerBound">Lower bound (single word only).</param>
        ISearchOverBetween<TRoot, TSubType> IsInclusivelyBetween(string lowerBound);

        /// <summary>
        /// Performs an exclusive range search to match field values that are between the lower and upper bound specified.
        /// Sorting is done lexicographically.
        /// </summary>
        /// <param name="lowerBound">Lower bound (single word only).</param>
        ISearchOverBetween<TRoot, TSubType> IsExclusivelyBetween(string lowerBound);
    }
}