using Dealership.Client.Commands.Abstract;
using Dealership.Client.Core.Abstract;
using Dealership.Data.Context;
using Dealership.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Dealership.Client.Commands.CRUD
{
    public class EditCommand : PrimeCommand
    {
        // edit [id]
        public override string Execute(string[] parameters)
        {
           
        }
    }
}
