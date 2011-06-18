using System;
using Lucene.Net.Analysis;

namespace FluentLucene.Mapping
{
    /// <summary>
    /// Represents a builder for a property of a target object
    /// </summary>
    public class PropertyBuilder : IInvertedPropertyBuilder<PropertyBuilder>
    {
        /// <summary>
        /// Gets the member used to get and set the value for a given target
        /// </summary>
        internal IMember Member { get; private set; }

        /// <summary>
        /// Gets the field name for the identity
        /// </summary>
        internal string FieldName { get; private set; }

        /// <summary>
        /// Whether the field is sortable
        /// </summary>
        internal bool? IsSortable { get; private set; }

        /// <summary>
        /// Gets the type of the analyzer to use for this field
        /// </summary>
        internal Type AnalyzerType { get; private set; }

        /// <summary>
        /// Gets the boost for this given field
        /// </summary>
        internal double? BoostValue { get; private set; }

        /// <summary>
        /// Creates an identity part for a given member
        /// </summary>
        /// <param name="member">The member used to get and set the value</param>
        internal PropertyBuilder(IMember member)
        {
            // Set the member
            Member = member;
            
            // Initialize the sub-builders
            index = new IndexBuilder<PropertyBuilder>(this);
            store = new StoreBuilder<PropertyBuilder>(this);
        }

        /// <summary>
        /// Specifies the name of the field in the index
        /// </summary>
        /// <param name="field">The name of the field</param>
        /// <returns>The builder itself</returns>
        public PropertyBuilder Field(string field)
        {
            // Sets the field name
            FieldName = field;

            // Return the identity part itself
            return this;
        }

        /// <summary>
        /// Backing field for the index builder
        /// </summary>
        private readonly IndexBuilder<PropertyBuilder> index;

        /// <summary>
        /// Allows to configure how the field is indexed
        /// </summary>
        public IndexBuilder<PropertyBuilder> Indexed { get { return index; } }

        /// <summary>
        /// Backing field for the index builder
        /// </summary>
        private readonly StoreBuilder<PropertyBuilder> store;

        /// <summary>
        /// Allows to configure how the field is stored in the index
        /// </summary>
        public StoreBuilder<PropertyBuilder> Stored { get { return store; } }

        /// <summary>
        /// Specify an analyzer to use specifically for this field
        /// </summary>
        /// <typeparam name="TAnalyzer">The type of the analyzer</typeparam>
        /// <returns>The builder itself</returns>
        public PropertyBuilder Analyzer<TAnalyzer>() where TAnalyzer : Analyzer
        {
            // Set the type of the analyzer
            AnalyzerType = typeof (TAnalyzer);

            // Return the instance of the builder
            return this;
        }

        /// <summary>
        /// Specify an analyzer to use specifically for this field
        /// </summary>
        /// <param name="analyzerType">The type of the analyzer</param>
        /// <returns>The builder itself</returns>
        public PropertyBuilder Analyzer(Type analyzerType)
        {
            // Set the type of the analyzer
            AnalyzerType = analyzerType;

            // Return the instance of the builder
            return this;
        }

        /// <summary>
        /// The inverter used to toggle inversion (Not)
        /// </summary>
        private readonly Inverter Inverter = new Inverter();

        /// <summary>
        /// Inverses the next operation.
        /// </summary>
        public IInvertedPropertyBuilder<PropertyBuilder> Not
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
        /// Specify that the field is sortable. The operation can be inverted by using <see cref="Not"/>.
        /// </summary>
        /// <returns>The builder itself</returns>
        public PropertyBuilder Sortable()
        {
            // Sets the value
            Inverter.SetAndReset(x => IsSortable = x, true);
            
            // Returns the instance of this builder
            return this;
        }

        /// <summary>
        /// Specify the boost value for the field
        /// </summary>
        /// <returns>The builder itself</returns>
        public PropertyBuilder Boost(double boost)
        {
            // Sets the value for the boost
            BoostValue = boost;

            // Returns the instance of this builder
            return this;
        }
    }
}