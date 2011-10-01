using System;
using Lucene.Net.Documents;

namespace FluentLucene.Types
{
    /// <summary>
    /// Mapping for <see cref="Enum"/>s.
    /// </summary>
    internal class EnumType : FieldType<object>
    {
        // TODO : Ensure that [Flag]Enums are mapped to underlying type (int, long)

        private readonly Type Type;

        /// <summary>
        /// Creates an EnumType by specifying the .NET type of the enum
        /// </summary>
        /// <param name="enumType">The type of the enum</param>
        public EnumType(Type enumType)
        {
            Type = enumType;
        }

        protected override object GetValueInternal(Field field)
        {
            return Enum.Parse(Type, field.StringValue());
        }

        protected override void SetValueInternal(Field field, object value)
        {
            field.SetValue(value.ToString());
        }

        public override bool IsLexicographicallyOrdered { get { return true; } }
    }
}