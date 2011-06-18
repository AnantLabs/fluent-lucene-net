namespace FluentLucene.Mapping
{
    /// <summary>
    /// Specifies whether and how a field should be indexed
    /// </summary>
    public enum FieldIndex
    {
        /// <summary>
        /// The field is not indexed
        /// </summary>
        No = 0,

        /// <summary>
        /// The field is indexed without Norms
        /// </summary>
        WithoutNorms,

        /// <summary>
        /// The field is indexed and tokenized with the analyzer
        /// </summary>
        Tokenized,

        /// <summary>
        /// The field is indexed as-is, untokenized
        /// </summary>
        AsIs
    }
}