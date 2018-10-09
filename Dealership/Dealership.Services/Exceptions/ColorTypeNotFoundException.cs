using System;
using System.Runtime.Serialization;

namespace Dealership.Services.Exceptions
{
    [Serializable]
    internal class ColorTypeNotFoundException : Exception
    {
        public ColorTypeNotFoundException()
        {
        }

        public ColorTypeNotFoundException(string message) : base(message)
        {
        }
    }
}