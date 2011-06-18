using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;

namespace FluentLucene.Mapping
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
    }

    public class PersonMap : SearchMap<Person>
    {
        public PersonMap()
        {
            Analyzer<StopAnalyzer>();
            Index.DefaultsTo.Tokenized();
            Storage.DefaultsTo.Yes();
            Sortable.DefaultsTo.Sortable();

            Id(x => x.Id);

            Map(x => x.Name).Boost(3)
                .Analyzer<StandardAnalyzer>()
                .Field("name")
                .Indexed.Tokenized()
                .Stored.Yes()
                .Sortable();

            Map(x => x.Comment);
        }
    }

    /// <summary>
    /// Base class for defining search mappings of a given entity 
    /// </summary>
    /// <typeparam name="T">The type of the entity</typeparam>
    public class SearchMap<T> : ISearchMap
    {
        /// <summary>
        /// Gets the identity current identity builder
        /// </summary>
        internal IdentityBuilder Identity { get; private set; }

        /// <summary>
        /// Backing field for the list of mappings
        /// </summary>
        private readonly List<PropertyBuilder> mappings = new List<PropertyBuilder>();

        /// <summary>
        /// The list of mappings defined
        /// </summary>
        public IList<PropertyBuilder> Mappings { get { return mappings; } }

        /// <summary>
        /// Get the type of the default analyzer
        /// </summary>
        public Type AnalyzerType { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SearchMap()
        {
            // Initialize the builders used for default values
            storage = CreateDefaultBuilder(new StoreBuilder<SearchMap<T>>(this));
            index = CreateDefaultBuilder(new IndexBuilder<SearchMap<T>>(this));
            sort = CreateDefaultBuilder(new SortBuilder<SearchMap<T>>(this));
        }

        /// <summary>
        /// Specify the identity for this entity
        /// </summary>
        /// <typeparam name="TProperty">The type of the accessor</typeparam>
        /// <param name="expression">The expression used to identify the accessor</param>
        /// <returns>The builder itself</returns>
        public IdentityBuilder Id<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            return Id(expression, null);
        }

        /// <summary>
        /// Specify the identity for this entity
        /// </summary>
        /// <typeparam name="TProperty">The type of the accessor</typeparam>
        /// <param name="expression">The expression used to identify the accessor</param>
        /// <param name="field">The field name to use in the index</param>
        /// <returns>The builder itself</returns>
        public IdentityBuilder Id<TProperty>(Expression<Func<T, TProperty>> expression, string field)
        {
            // Ensures the identity is not already defined
            if (Identity != null) throw new InvalidOperationException("The identity is already defined");

            // Creates and sets the identity builder
            Identity = new IdentityBuilder(expression.ToMember());

            // Set the name of the field
            Identity.Field(field);

            // Return the identity
            return Identity;
        }

        /// <summary>
        /// Specify a mapping for a given member of the entity
        /// </summary>
        /// <typeparam name="TProperty">The type of the accessor</typeparam>
        /// <param name="expression">The expression used to identify the accessor</param>
        /// <returns>The builder itself</returns>
        public PropertyBuilder Map<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            return Map(expression, null);
        }

        /// <summary>
        /// Specify a mapping for a given member of the entity
        /// </summary>
        /// <typeparam name="TProperty">The type of the accessor</typeparam>
        /// <param name="expression">The expression used to identify the accessor</param>
        /// <param name="field">The field name to use in the index</param>
        /// <returns>The builder itself</returns>
        public PropertyBuilder Map<TProperty>(Expression<Func<T, TProperty>> expression, string field)
        {
            // Create a new mapping
            var mapping = new PropertyBuilder(expression.ToMember());

            // Set the name of the field for the mapping
            mapping.Field(field);

            // Add the mapping to the existing mappings
            Mappings.Add(mapping);

            // Return the newly created mapping
            return mapping;
        }

        /// <summary>
        /// Specify an analyzer to use specifically for this field
        /// </summary>
        /// <typeparam name="TAnalyzer">The type of the analyzer</typeparam>
        /// <returns>The builder itself</returns>
        public SearchMap<T> Analyzer<TAnalyzer>() where TAnalyzer : Analyzer
        {
            // Set the type of the analyzer
            AnalyzerType = typeof(TAnalyzer);

            // Return the instance of the builder
            return this;
        }

        /// <summary>
        /// Specify an analyzer to use specifically for this field
        /// </summary>
        /// <param name="analyzerType">The type of the analyzer</param>
        /// <returns>The builder itself</returns>
        public SearchMap<T> Analyzer(Type analyzerType)
        {
            // Set the type of the analyzer
            AnalyzerType = analyzerType;

            // Return the instance of the builder
            return this;
        }

        #region Defaults

        /// <summary>
        /// Creates a builder for default values
        /// </summary>
        /// <typeparam name="TBuilder">The type of the builder used for default values</typeparam>
        /// <param name="builder">The builder</param>
        /// <returns>The builder of default values for the given builder</returns>
        private static DefaultValueBuilder<TBuilder> CreateDefaultBuilder<TBuilder>(TBuilder builder) where TBuilder : class
        {
            // Wrap the builder in a default value builder
            return new DefaultValueBuilder<TBuilder>(builder);
        }

        /// <summary>
        /// Backing field for the default indexing configuration 
        /// </summary>
        private readonly DefaultValueBuilder<IndexBuilder<SearchMap<T>>> index;

        /// <summary>
        /// Allows the configure the default indexing configuration for this entity
        /// </summary>
        public DefaultValueBuilder<IndexBuilder<SearchMap<T>>> Index { get { return index; } }

        /// <summary>
        /// Backing field for the default storage configuration
        /// </summary>
        private readonly DefaultValueBuilder<StoreBuilder<SearchMap<T>>> storage;

        /// <summary>
        /// Allows the configure the default storage configuration for this entity
        /// </summary>
        public DefaultValueBuilder<StoreBuilder<SearchMap<T>>> Storage { get { return storage; } }

        /// <summary>
        /// Backing field for the default sorting configuration
        /// </summary>
        private readonly DefaultValueBuilder<SortBuilder<SearchMap<T>>> sort;

        /// <summary>
        /// Allows to configure the default sorting configuration for this entity
        /// </summary>
        public DefaultValueBuilder<SortBuilder<SearchMap<T>>> Sortable { get { return sort; } }

        #endregion

        #region Implementation fo ISearchMap

        // Explicit implementation of the ISeachMap interface allowing decoupling from the format of the fluent interface
        // while mainaining simplicity

        /// <summary>
        /// Gets the type of the document
        /// </summary>
        Type ISearchMap.DocumentType { get { return typeof (T); } }

        /// <summary>
        /// Gets the identity current identity builder
        /// </summary>
        IdentityBuilder ISearchMap.Identity { get { return Identity; } }

        /// <summary>
        /// The list of mappings defined
        /// </summary>
        IList<PropertyBuilder> ISearchMap.Mappings { get { return Mappings; } }

        /// <summary>
        /// Get the type of the default analyzer
        /// </summary>
        Type ISearchMap.AnalyzerType { get { return AnalyzerType; } }

        /// <summary>
        /// Allows the configure the default indexing configuration for this entity
        /// </summary>
        IndexBuilder ISearchMap.Index { get { return Index.DefaultsTo; } }

        /// <summary>
        /// Allows the configure the default storage configuration for this entity
        /// </summary>
        StoreBuilder ISearchMap.Storage { get { return Storage.DefaultsTo; } }

        /// <summary>
        /// Allows to configure the default sorting configuration for this entity
        /// </summary>
        SortBuilder ISearchMap.Sortable { get { return sort.DefaultsTo; } }

        #endregion
    }
}