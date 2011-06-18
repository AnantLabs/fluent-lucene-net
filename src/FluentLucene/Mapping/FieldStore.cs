namespace FluentLucene.Mapping
{
    /// <summary>
    /// Specifies whether and how a field should be stored
    /// </summary>
    public enum FieldStore
    {
        /// <summary>
        /// The field is not stored
        /// </summary>
        No = 0,

        /// <summary>
        /// The field is stored, uncompressed
        /// </summary>
        Yes,

        /// <summary>
        /// The field is stored and compressed
        /// </summary>
        Compressed
    }
}