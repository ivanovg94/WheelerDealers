using System;

namespace Dealership.Services.Exceptions
{
    [Serializable]
    public class FuelNotFoundException : Exception
    {
        public FuelNotFoundException()
        {
        }

        public FuelNotFoundException(string message) : base(message)
        {
        }
    }
}
