using System;

namespace Dealership.Services.Exceptions
{
    [Serializable]
    public class GearboxNotFoundException : Exception
    {
        public GearboxNotFoundException()
        {
        }

        public GearboxNotFoundException(string message) : base(message)
        {
        }
    }
}
