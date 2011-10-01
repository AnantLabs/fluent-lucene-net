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
        private readonly IDictionary<Type, IType> Types = new Dictionary<Type, IType>(); 

        public TypeProvider()
        {
            
        }

        private void RegisterTypes()
        {
            Register<bool>(new BooleanType());
            Register<byte>(new ByteType());
            Register<char>(new CharType());
            Register<DateTime>(new DateTimeType());
            Register<decimal>(new DecimalType());
            Register<double>(new DoubleType());
            Register<short>(new Int16Type());
            Register<int>(new Int32Type());
            Register<long>(new Int64Type());
            Register<sbyte>(new SByteType());
            Register<float>(new SingleType());
            Register<string>(new StringType());
            Register<TimeSpan>(new TimeSpanType());
            Register<ushort>(new Int16Type());
            Register<uint>(new Int32Type());
            Register<ulong>(new Int64Type());
        }

        private void Register<T>(IType mappingType)
        {
            Types.Add(typeof(T), mappingType);
        }

        /// <summary>
        /// Gets a mapping for the given .NET type
        /// </summary>
        /// <param name="type">The .NET type</param>
        /// <returns>The mapping type, or null</returns>
        public IType GetFor(Type type)
        {
            throw new NotImplementedException();
        }
    }
}