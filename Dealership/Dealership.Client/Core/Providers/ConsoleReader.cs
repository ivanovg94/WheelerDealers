using Dealership.Client.Core.Abstract;
using System;

namespace Dealership.Client.Core.Providers
{
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
