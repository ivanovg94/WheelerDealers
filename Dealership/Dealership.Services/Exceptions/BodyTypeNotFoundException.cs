using System;
using System.Runtime.Serialization;

namespace Dealership.Services.Exceptions
{
    [Serializable]
    internal class BodyTypeNotFoundException : Exception
    {
        public BodyTypeNotFoundException()
        {
        }

        public BodyTypeNotFoundException(string message) : base(message)
        {
        }
    }
}