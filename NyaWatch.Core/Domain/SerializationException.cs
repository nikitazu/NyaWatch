using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NyaWatch.Core.Domain
{
    public abstract class SerializationException : Exception
    {
        public SerializationException (Exception inner) : base(string.Empty, inner)
        {
            // empty
        }
    }

    public class SerializeFailedException : SerializationException
    {
        public IAnime SerializationData { get; private set; }

        public SerializeFailedException (IAnime data, Exception inner) 
            : base(inner)
        {
            SerializationData = data;
        }

        public override string Message
        {
            get
            {
                return SerializationData == null ? 
                    "Serialization failed and serialization data was NULL (see inner exception)" :
                    "Serialization failed (see inner exception)";
            }
        }
    }

    public class DeserializeFailedException : SerializationException
    {
        public IDictionary<string, string> SerializationData { get; private set; }

        public DeserializeFailedException (IDictionary<string, string> data, Exception inner)
            : base (inner)
        {
            SerializationData = data;
        }

        public override string Message
        {
            get
            {
                return SerializationData == null ?
                    "Deserialization failed and serialization data was NULL (see inner exception)" :
                    "Deserialization failed (see inner exception)";
            }
        }
    }
}
