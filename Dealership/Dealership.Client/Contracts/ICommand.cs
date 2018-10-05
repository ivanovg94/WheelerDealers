using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Contracts
{
    public interface ICommand
    {
        string ProcessCommand(string[] parameters);
    }
}
