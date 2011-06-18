
namespace FluentLucene.Mapping
{
    /// <summary>
    /// Builder for defining the default values for a given builder
    /// </summary>
    /// <typeparam name="TBuilder">The builder's type</typeparam>
    public class DefaultValueBuilder<TBuilder> where TBuilder : class
    {
        /// <summary>
        /// The builder
        /// </summary>
        private readonly TBuilder Builder;

        /// <summary>
        /// Creates a builder for default values
        /// </summary>
        /// <param name="builder">The builder</param>
        internal DefaultValueBuilder(TBuilder builder)
        {
            Builder = builder;
        }

        /// <summary>
        /// Allows to define the default configuration
        /// </summary>
        public TBuilder DefaultsTo { get { return Builder; } }
    }
}