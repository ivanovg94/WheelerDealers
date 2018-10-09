using System;

namespace Dealership.Services.Exceptions
{
    [Serializable]
    public class CarNotFoundException : Exception
    {
        public CarNotFoundException()
        {
        }

        public CarNotFoundException(string message) : base(message)
        {
        }
    }
}
