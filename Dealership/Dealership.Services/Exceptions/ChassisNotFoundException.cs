using System;
using System.Runtime.Serialization;

namespace Dealership.Services.Exceptions
{
    [Serializable]
    internal class ChassisNotFoundException : Exception
    {
        public ChassisNotFoundException()
        {
        }

        public ChassisNotFoundException(string message) : base(message)
        {
        }
    }
}