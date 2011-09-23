using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentLucene.MappingModel
{
    /// <summary>
    /// The mapping representing a whole document
    /// </summary>
    internal class DocumentMapping
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DocumentMapping()
        { }

        /// <summary>
        /// Create a document mapping with an identity mappings and field mappings
        /// </summary>
        /// <param name="documentType">The type of the document mapped</param>
        /// <param name="identity">The identity mappings</param>
        /// <param name="fields">The fields mappings</param>
        public DocumentMapping(Type documentType, IdentityMapping identity, IEnumerable<FieldMapping> fields)
        {
            DocumentType = documentType;
            Identity = identity;
            this.fields.AddRange(fields);

            allFields.Add(identity);
            allFields.AddRange(this.fields);
        }

        /// <summary>
        /// Gets or sets the type of the document mapped
        /// </summary>
        public Type DocumentType { get; private set; }

        /// <summary>
        /// Gets or sets the mapping for the identity
        /// </summary>
        public IdentityMapping Identity { get; private set; }

        /// <summary>
        /// Backing field for the field mappings
        /// </summary>
        private readonly List<FieldMapping> fields = new List<FieldMapping>();

        /// <summary>
        /// Gets the mappings for every field
        /// </summary>
        public IEnumerable<FieldMapping> Fields { get { return fields; } }

        private readonly List<IFieldLikeMapping> allFields = new List<IFieldLikeMapping>();

        /// <summary>
        /// Get every field-like mappings. These mappings should be used when materializing a document comming from Lucene.
        /// </summary>
        public IEnumerable<IFieldLikeMapping> AllFieldLikeMappings { get { return allFields; } }

        /// <summary>
        /// Gets a field mapping by name or null if no field has the given name
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <returns>The field mapping or null</returns>
        public IFieldLikeMapping Field(string name)
        {
            return AllFieldLikeMappings.FirstOrDefault(x => x.Name == name);
        }
    }
}