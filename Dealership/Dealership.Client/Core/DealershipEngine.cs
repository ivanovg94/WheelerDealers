using Autofac;
using Dealership.Client.Contracts.Abstract;
using Dealership.Client.Core.Abstract;
using Dealership.Data.Models.Contracts;
using System;
using System.Linq;


namespace Dealership.Client.Core
{
    public class DealershipEngine : IEngine
    {
        private IComponentContext containerContext;
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly IUserSession userSession;

        public DealershipEngine(IComponentContext containerContext, IReader reader, IWriter writer, IUserSession userSession)
        {
            this.containerContext = containerContext;
            this.reader = reader;
            this.writer = writer;
            this.userSession = userSession;
        }

        string input = string.Empty;

        public void Run()
        {
            while ((input = reader.ReadLine()) != "exit")
            {
                try
                {
                    var inputParams = input.Split();
                    string commandName = inputParams[0].ToLower();

                    if (userSession.CurrentUser == null && commandName != "login" && commandName != "register")
                    {
                        throw new InvalidOperationException("Please login or register.");
                    }

                    var command = this.ParseCommand(commandName);
                    var commandResult = command.Execute(inputParams.Skip(1).ToArray());

                    writer.WriteLine(commandResult);
                }
                catch (Exception ex)
                {
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }
                    Console.WriteLine(ex.Message);
                    ExceptionLogging.SendErrorToText(ex);
                }
            }
        }

        private ICommand ParseCommand(string commandStr)
        {
            try
            {
                return this.containerContext.ResolveNamed<ICommand>(commandStr);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("The entered command is invalid!");
            }
        }
    }
}
