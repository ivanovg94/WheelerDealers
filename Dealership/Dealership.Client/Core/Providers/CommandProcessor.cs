using Dealership.Client.Core.Abstract;
using Dealership.Data.Models.Contracts;
using System;
using System.Linq;

namespace Dealership.Client.Core.Providers
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly ICommandParser commandParser;
        private readonly IUserSession userSession;
        public CommandProcessor(ICommandParser commandParser, IUserSession userSession)
        {
            this.commandParser = commandParser;
            this.userSession = userSession;
        }

        public string ProcessCommand(string input)
        {
            var inputParams = input.Split();
            string commandName = inputParams[0].ToLower();
            var args = inputParams.Skip(1).ToArray();

            if (userSession.CurrentUser == null && commandName != "login" && commandName != "register")
            {
                throw new InvalidOperationException("Please login or register.");
            }

            var command = this.commandParser.ParseCommand(commandName);
            return command.Execute(args);
        }
    }
}
