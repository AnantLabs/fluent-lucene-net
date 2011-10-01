using System;
using System.Runtime.Serialization;

namespace FluentLucene
{
    /// <summary>
    /// Exception for when a provided type is not supported by FluentLucene
    /// </summary>
    [Serializable]
    public class TypeNotSupportedException : FluentLuceneException
    {
        public TypeNotSupportedException()
        { }

        public TypeNotSupportedException(string message) 
            : base(message)
        { }

        public TypeNotSupportedException(string message, Exception inner) 
            : base(message, inner)
        { }

        protected TypeNotSupportedException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        { }
    }
}