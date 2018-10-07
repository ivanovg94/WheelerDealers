using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Contracts.Abstract
{
    public interface ICommand
    {
        string Execute(string[] parameters);
    }
}
