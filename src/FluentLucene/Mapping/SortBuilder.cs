namespace FluentLucene.Mapping
{
    /// <summary>
    /// Builder used to configure how to sort fields
    /// </summary>
    public class SortBuilder
    {
        /// <summary>
        /// Gets or sets whether the field is sortable or not
        /// </summary>
        internal bool? IsSortable { get; set; }
    }

    /// <summary>
    /// Builder used to configure how to sort fields
    /// </summary>
    /// <typeparam name="TPrevious">The return type of fluent methods</typeparam>
    public class SortBuilder<TPrevious> : SortBuilder
    {
        /// <summary>
        /// The instance to return from fluent methods
        /// </summary>
        private readonly TPrevious Previous;

        /// <summary>
        /// Creates an sort builder by specifying the instance to return from fluent methods
        /// </summary>
        /// <param name="previous">The instance to return from fluent methods</param>
        internal SortBuilder(TPrevious previous)
        {
            Previous = previous;
        }

        /// <summary>
        /// The inverter used to toggle inversion (Not)
        /// </summary>
        private readonly Inverter Inverter = new Inverter();

        /// <summary>
        /// Inverses the next operation.
        /// </summary>
        public SortBuilder<TPrevious> Not
        {
            get
            {
                // Toggles the value of the inverter
                Inverter.Toggle();

                // Returns the instance of this builder
                return this;
            }
        }

        /// <summary>
        /// Sets that the field is sortable. The operation can be inverted by using <see cref="Not"/>.
        /// </summary>
        public TPrevious Sortable()
        {
            // Sets the value
            Inverter.SetAndReset(x => IsSortable = x, true);

            // Returns the instance of the previous builder
            return Previous;
        }
    }
}