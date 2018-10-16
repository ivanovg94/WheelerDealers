using System;

namespace Dealership.Services.Exceptions
{
    [Serializable]
    public class BrandNotFoundException : Exception
    {
        public BrandNotFoundException()
        {
        }

        public BrandNotFoundException(string message) : base(message)
        {
        }
    }
}
