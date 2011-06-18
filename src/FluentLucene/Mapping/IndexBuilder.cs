namespace FluentLucene.Mapping
{
    /// <summary>
    /// Builder to configure how to index the fields
    /// </summary>
    public class IndexBuilder
    {
        /// <summary>
        /// Gets or sets how to index the field
        /// </summary>
        internal FieldIndex? Index { get; set; }
    }

    /// <summary>
    /// Builder to configure how to index the fields
    /// </summary>
    /// <typeparam name="TPrevious">The return type of fluent methods</typeparam>
    public class IndexBuilder<TPrevious> : IndexBuilder
    {
        /// <summary>
        /// The instance to return from fluent methods
        /// </summary>
        private readonly TPrevious Previous;

        /// <summary>
        /// Creates an index builder by specifying the instance to return from fluent methods
        /// </summary>
        /// <param name="previous">The instance to return from fluent methods</param>
        internal IndexBuilder(TPrevious previous)
        {
            Previous = previous;
        }

        /// <summary>
        /// The field is not indexed
        /// </summary>
        public TPrevious No()
        {
            Index = FieldIndex.No;
            return Previous;
        }

        /// <summary>
        /// The field is indexed without Norms
        /// </summary>
        public TPrevious WithoutNorms()
        {
            Index = FieldIndex.WithoutNorms;
            return Previous;
        }

        /// <summary>
        /// The field is indexed and tokenized with the analyzer
        /// </summary>
        public TPrevious Tokenized()
        {
            Index = FieldIndex.Tokenized;
            return Previous;
        }
        /// <summary>
        /// The field is indexed as-is, untokenized
        /// </summary>
        public TPrevious AsIs()
        {
            Index = FieldIndex.AsIs;
            return Previous;
        }

        /// <summary>
        /// The field is indexed according to the specified <see cref="FieldIndex"/>
        /// </summary>
        /// <param name="index">How to index the field</param>
        public TPrevious As(FieldIndex index)
        {
            Index = index;
            return Previous;
        }
    }
}