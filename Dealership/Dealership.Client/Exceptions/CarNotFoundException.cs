using System;

namespace Dealership.Client.Exceptions
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
