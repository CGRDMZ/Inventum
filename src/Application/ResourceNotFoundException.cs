using System;
using System.Runtime.Serialization;

namespace Application
{
    [System.Serializable]
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException() { }
        public ResourceNotFoundException(string message) : base(message) { }
        public ResourceNotFoundException(string message, System.Exception inner) : base(message, inner) { }
        protected ResourceNotFoundException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}