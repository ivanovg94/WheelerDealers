using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Core.Abstract
{
    public interface ICommand
    {
        string ProcessCommand(string[] parameters);
    }
}
