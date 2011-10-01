using System;
using System.Collections.Generic;
using FluentLucene.Types;

namespace FluentLucene.Materialize
{
    /// <summary>
    /// Implementation of a provider for different mapping types
    /// </summary>
    internal class TypeProvider : ITypeProvider
    {
        /// <summary>
        /// Contains default mappings for what is supported natively by FluentLucene
        /// </summary>
        private readonly IDictionary<Type, Func<Type, IType>> NativeTypes = new Dictionary<Type, Func<Type, IType>>(); 

        public TypeProvider()
        {
            RegisterNativeTypes();
        }

        /// <summary>
        /// Gets a mapping for the given .NET type
        /// </summary>
        /// <param name="type">The .NET type</param>
        /// <returns>The mapping type, or null</returns>
        public IType GetFor(Type type)
        {
            var knownType = type;

            // The type is a nullable, get the underlying type
            if (type.IsGenericType && Equals(type.GetGenericTypeDefinition(), typeof(Nullable<>)))
            {
                knownType = Nullable.GetUnderlyingType(type);
            }

            // Try and get a factory method for natively supported types
            Func<Type, IType> factoryMethod;
            if (NativeTypes.TryGetValue(knownType, out factoryMethod))
            {
                // The type is natively supported
                return factoryMethod(knownType);
            }

            // If the type is an enum, get the EnumType
            if (knownType.IsEnum)
            {
                return NativeTypes[typeof(Enum)](knownType);
            }

            throw new TypeNotSupportedException(string.Format(Messages.TypeNotSupported1, type));
        }

        private void RegisterNativeTypes()
        {
            Register<bool>(t => new BooleanType());
            Register<byte>(t => new ByteType());
            Register<char>(t => new CharType());
            Register<decimal>(t => new DecimalType());
            Register<double>(t => new DoubleType());
            Register<short>(t => new Int16Type());
            Register<int>(t => new Int32Type());
            Register<long>(t => new Int64Type());
            Register<sbyte>(t => new SByteType());
            Register<float>(t => new SingleType());
            Register<string>(t => new StringType());
            Register<ushort>(t => new UInt16Type());
            Register<uint>(t => new UInt32Type());
            Register<ulong>(t => new UInt64Type());

            Register<Enum>(t => new EnumType(t));

            Register<DateTime>(t => new DateTimeType());
            Register<TimeSpan>(t => new TimeSpanType());
        }

        private void Register<T>(Func<Type, IType> factoryMethod)
        {
            NativeTypes.Add(typeof(T), factoryMethod);
        }
    }
}