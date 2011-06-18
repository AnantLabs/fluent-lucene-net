namespace FluentLucene.Mapping.Resolution
{
    /// <summary>
    /// The different types of resolution
    /// </summary>
    public enum ResolutionType
    {
        /// <summary>
        /// Represents the abscence of a resolution type
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// A field is marked as sortable but is not indexed as <see cref="FieldIndex.AsIs"/>
        /// </summary>
        SortableFieldNotIndexedAsIs,

        /// <summary>
        /// A field is marked as sortable but is not stored in the index
        /// </summary>
        SortableFieldNotStored,
    }
}