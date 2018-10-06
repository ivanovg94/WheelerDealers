using Autofac;
using Dealership.Client.Contracts;
using Dealership.Client.Contracts.Abstract;
using Dealership.Client.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Client.Core
{
    public class DealershipEngine : IEngine
    {
        private IComponentContext containerContext;
        string input = string.Empty;

        public DealershipEngine(IComponentContext containerContext)
        {
            this.containerContext = containerContext;
        }
        public void Run()
        {

            while ((input = Console.ReadLine()) != "exit")
            {
                try
                {
                    var inputParams = input.Split();
                    var command = this.CommandParser(inputParams[0]);
                    //commands not implemented yet !
                    //commandResult returns message according to operation result (successful or not)
                    var commandResult = command.Execute(inputParams.Skip(1).ToArray());

                    Console.WriteLine(commandResult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }               
            }
        }

        private ICommand CommandParser(string commandStr)
        {
            return this.containerContext.ResolveNamed<ICommand>(commandStr);
        }
    }
}
