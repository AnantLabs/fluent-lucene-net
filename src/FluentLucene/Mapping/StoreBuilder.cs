
namespace FluentLucene.Mapping
{
    /// <summary>
    /// Builder used to configure how to store fields
    /// </summary>
    public class StoreBuilder
    {
        /// <summary>
        /// Gets or sets how to store the field
        /// </summary>
        internal FieldStore? Store { get; set; }
    }

    /// <summary>
    /// Builder used to configure how to store fields
    /// </summary>
    /// <typeparam name="TPrevious">The return type of fluent methods</typeparam>
    public class StoreBuilder<TPrevious> : StoreBuilder
    {
        /// <summary>
        /// The instance to return from fluent methods
        /// </summary>
        private readonly TPrevious Previous;

        /// <summary>
        /// Creates an sort builder by specifying the instance to return from fluent methods
        /// </summary>
        /// <param name="previous">The instance to return from fluent methods</param>
        internal StoreBuilder(TPrevious previous)
        {
            Previous = previous;
        }

        /// <summary>
        /// The field is not stored
        /// </summary>
        public TPrevious No()
        {
            Store = FieldStore.No;
            return Previous;
        }

        /// <summary>
        /// The field is stored, uncompressed
        /// </summary>
        public TPrevious Yes()
        {
            Store = FieldStore.Yes;
            return Previous;
        }

        /// <summary>
        /// The field is stored and compressed
        /// </summary>
        public TPrevious Compressed()
        {
            Store = FieldStore.Compressed;
            return Previous;
        }

        /// <summary>
        /// The field is stored according to the specified <see cref="FieldStore"/>
        /// </summary>
        /// <param name="store">How to store the field</param>
        public TPrevious As(FieldStore store)
        {
            Store = store;
            return Previous;
        }
    }
}