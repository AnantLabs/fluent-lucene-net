using System;
using System.Globalization;
using FluentLucene.MappingModel;
using Lucene.Net.Documents;

namespace FluentLucene.Materialize
{
    /// <summary>
    /// Basic implementation of a materializer
    /// </summary>
    internal class SimpleMaterializer : IMaterializer
    {
        private readonly IEntityActivator Activator;
        private readonly ITypeFactory TypeFactory;

        public SimpleMaterializer(IEntityActivator activator, ITypeFactory typeFactory)
        {
            Activator = activator;
            TypeFactory = typeFactory;
        }

        /// <summary>
        /// Materializes an entity from mappings and a given document
        /// </summary>
        /// <param name="mapping">The mappings describing how materialize the document</param>
        /// <param name="document">The Lucene document containing information to materialize</param>
        /// <returns>The materialized entity</returns>
        public object Materialize(DocumentMapping mapping, Document document)
        {
            // Create a new object to hydrate
            var result = Activator.CreateInstance(mapping.DocumentType);

            // Set the value of all mapped fields
            foreach (var map in mapping.AllFieldLikeMappings)
            {
                var field = document.GetFieldable(map.Name);

                if (field != null)
                {
                    var value = ParseValue(field.StringValue(), map.Type);
                    map.SetValue(result, value);
                }
            }

            return result;
        }

        /// <summary>
        /// Parses a string value into the correct type.
        /// </summary>
        /// <param name="stringValue">The string value</param>
        /// <param name="mappingType">The expected type of the value</param>
        /// <returns>The parsed value</returns>
        /// <exception cref="NotSupportedException">If the type of the value is not supported</exception>
        public object ParseValue(string stringValue, Type mappingType)
        {
            // TODO : Build an infrastructure for mappings .NET types and values in the index (much like NHibernate's IType)

            // This is just a hackerish, quick way to parse lots of possible types

            var culture = CultureInfo.InvariantCulture;
            var type = mappingType;

            // The value is null
            if (string.IsNullOrEmpty(stringValue)) return null;

            if (type.IsGenericType)
            {
                var genericType = type.GetGenericTypeDefinition();

                if (genericType == typeof(Nullable<>))
                {
                    type = Nullable.GetUnderlyingType(type);
                }
            }

            object value;

            if (type == typeof(string)) value = stringValue;
            else if (type == typeof(char)) value = char.Parse(stringValue);
            else if (type == typeof(bool)) value = bool.Parse(stringValue);
            else if (type == typeof(decimal)) value = decimal.Parse(stringValue, culture);
            else if (type == typeof(double)) value = double.Parse(stringValue, culture);
            else if (type == typeof(float)) value = float.Parse(stringValue, culture);
            else if (type == typeof(int)) value = int.Parse(stringValue, culture);
            else if (type == typeof(uint)) value = uint.Parse(stringValue, culture);
            else if (type == typeof(long)) value = long.Parse(stringValue, culture);
            else if (type == typeof(ulong)) value = ulong.Parse(stringValue, culture);
            else if (type == typeof(short)) value = short.Parse(stringValue, culture);
            else if (type == typeof(ushort)) value = ushort.Parse(stringValue, culture);
            else if (type == typeof(byte)) value = byte.Parse(stringValue, culture);
            else if (type == typeof(sbyte)) value = sbyte.Parse(stringValue, culture);
            else if (type == typeof(DateTime)) value = DateTime.ParseExact(stringValue, "o", culture);
            else if (type == typeof(TimeSpan)) value = TimeSpan.Parse(stringValue, culture);
            else if (type.IsEnum) value = Enum.Parse(type, stringValue);
            else throw new NotSupportedException(string.Format("The type of {0} is not supported for mapping.", type.FullName));

            // No need to wrap the value type in a Nullable<type> (in fact it appears to be near-impossible to do so)
            // The value will be assigned just fine later on

            return value;
        }
    }
}