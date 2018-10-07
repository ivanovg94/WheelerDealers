using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Exceptions
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
