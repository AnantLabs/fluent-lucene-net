using System;
using System.Collections;
using System.Collections.Generic;
using FluentLucene.Infrastructure;
using FluentLucene.Types;

namespace FluentLucene.Materialize
{
    /// <summary>
    /// Implementation of a factorys for different mapping types
    /// </summary>
    internal class TypeFactory : ITypeFactory
    {
        /// <summary>
        /// Contains default mappings for what is supported natively by FluentLucene
        /// </summary>
        private readonly IDictionary<Type, Type> NativeTypes = new Dictionary<Type, Type>();

        private readonly IServiceLocator ServiceLocator;

        public TypeFactory(IServiceLocator serviceLocator)
        {
            ServiceLocator = serviceLocator;
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
            Type mappingType;
            if (NativeTypes.TryGetValue(knownType, out mappingType))
            {
                // The type is natively supported
                return (IType)ServiceLocator.Get(mappingType);
            }

            // If the type is an enum, get the EnumType
            if (knownType.IsEnum)
            {
                return ServiceLocator.Get<EnumType>(new Hashtable { { "enumType", knownType } });
            }

            throw new TypeNotSupportedException(string.Format(Messages.TypeNotSupported1, type));
        }

        private void RegisterNativeTypes()
        {
            Register<bool, BooleanType>();
            Register<byte, ByteType>();
            Register<char, CharType>();
            Register<decimal, DecimalType>();
            Register<double, DoubleType>();
            Register<short, Int16Type>();
            Register<int, Int32Type>();
            Register<long, Int64Type>();
            Register<sbyte, SByteType>();
            Register<float, SingleType>();
            Register<string, StringType>();
            Register<ushort, UInt16Type>();
            Register<uint, UInt32Type>();
            Register<ulong, UInt64Type>();

            Register<Enum, EnumType>();

            Register<DateTime, DateTimeType>();
            Register<TimeSpan, TimeSpanType>();
        }

        private void Register<TClr, TMapping>()
        {
            NativeTypes.Add(typeof(TClr), typeof(TMapping));
            ServiceLocator.Container.Transient<TMapping>();
        }
    }
}