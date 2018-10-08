using Dealership.Client.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Core.Providers
{
    public class ConsoleWriter : IWriter
    {
        public void Write(string message)
        {
            Console.Write(message);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
