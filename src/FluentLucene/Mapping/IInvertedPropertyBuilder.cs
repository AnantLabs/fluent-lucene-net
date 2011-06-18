namespace FluentLucene.Mapping
{
    /// <summary>
    /// Represents availaible on a property builder when inverted
    /// </summary>
    /// <typeparam name="TPrevious">The type of the previous builder</typeparam>
    public interface IInvertedPropertyBuilder<out TPrevious>
    {
        /// <summary>
        /// Specify that the field is sortable.
        /// </summary>
        /// <returns>The builder itself</returns>
        TPrevious Sortable();
    }
}