using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentLucene.Search
{
    /// <summary>
    /// SearchOver is an API for searching entities using Lambda expression syntax.
    /// </summary>
    public interface ISearchOver<TRoot, TSubType> : ISearchOver<TRoot>
    {
        /// <summary>
        /// Identical semantics to AndAny() to allow more readable queries
        /// </summary>
        ISearchOverField<TRoot, TSubType> WhereAny();

        /// <summary>
        /// Identical semantics to And() to allow more readable queries
        /// </summary>
        /// <param name="expression">Lambda expression containing path to property</param>
        ISearchOverField<TRoot, TSubType> Where(Expression<Func<TRoot, object>> expression);

        /// <summary>
        /// Add search restriction that applies to any of the default search fields.
        /// </summary>
        ISearchOverField<TRoot, TSubType> AndAny();

        /// <summary>
        /// Add search restriction to a field.
        /// </summary>
        /// <param name="expression">Lambda expression containing path to property</param>
        ISearchOverField<TRoot, TSubType> And(Expression<Func<TRoot, object>> expression);

        /// <summary>
        /// Add an alternate search restriction to a field.
        /// </summary>
        /// <param name="expression">Lambda expression containing path to property</param>
        ISearchOverField<TRoot, TSubType> Or(Expression<Func<TRoot, object>> expression);

        /// <summary>
        /// Add an alternate search restriction that applies to any of the default search fields.
        /// </summary>
        ISearchOverField<TRoot, TSubType> OrAny();

        /// <summary>
        /// Add search restriction that applies to the field.
        /// </summary>
        ISearchOverField<TRoot, TSubType> And();
    }

    /// <summary>
    /// SearchOver&lt;TRoot&gt; is an API for searching entities using Lambda expression syntax.
    /// </summary>
    public interface ISearchOver<TRoot>
    {
        /// <summary>
        /// Get the results of the root type and fill the <see cref="IList{TRoot}"/>
        /// </summary>
        /// <returns>The list filled with the results.</returns>
        IList<TRoot> List();

        /// <summary>
        /// Get the results of the root type and fill the <see cref="IList{T}"/>
        /// </summary>
        /// <returns>The list filled with the results.</returns>
        IList<T> List<T>();

        /// <summary>
        /// Set a limit upon the number of objects to be retrieved
        /// </summary>
        /// <param name="maxResults">Maximum number of results</param>
        ISearchOver<TRoot> Take(int maxResults);

        /// <summary>
        /// Add order expressed as a lambda expression
        /// </summary>
        /// <param name="path">Lambda expression</param>
        ISearchOver<TRoot> OrderBy(Expression<Func<TRoot, object>> path);
    }
}