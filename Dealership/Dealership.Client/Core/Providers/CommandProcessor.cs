using Dealership.Client.Core.Abstract;
using Dealership.Data.Models.Contracts;
using System;
using System.Linq;

namespace Dealership.Client.Core.Providers
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly ICommandParser commandParser;
        public CommandProcessor(ICommandParser commandParser)
        {
            this.commandParser = commandParser;
        }

        public string ProcessCommand(string input)
        {
            var inputParams = input.Split();
            string commandName = inputParams[0].ToLower();
            var args = inputParams.Skip(1).ToArray();

            var command = this.commandParser.ParseCommand(commandName);
            return command.Execute(args);
        }
    }
}
