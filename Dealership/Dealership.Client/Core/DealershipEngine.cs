using Dealership.Client.Core.Abstract;
using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using System;

namespace Dealership.Client.Core
{
    public class DealershipEngine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IExceptionLogging exceptionLogging;
        private readonly ICommandProcessor processor;
        private readonly IUserSession userSession;
        public DealershipEngine(IReader reader, IWriter writer, IUserSession userSession,
            IExceptionLogging exceptionLogging, ICommandProcessor commandProcessor)
        {
            this.reader = reader;
            this.writer = writer;
            this.exceptionLogging = exceptionLogging;
            this.processor = commandProcessor;
            this.userSession = userSession;
        }

        string input = string.Empty;

        public void Run()
        {
            this.writer.PrintCommands();
            while ((input = reader.ReadLine()) != "exit")
            {
                try
                {
                    if (this.userSession.CurrentUser == null
                        && !input.ToLower().StartsWith("login")
                        && !input.ToLower().StartsWith("register"))
                    {
                        throw new InvalidOperationException("Please login or register.");
                    }

                    //if (this.userSession.CurrentUser == null)
                    //{
                    //    this.writer.PrintLoginCommands();
                    //}
                    //else if (this.userSession.CurrentUser.UserType == UserType.Admin)
                    //{
                    //    this.writer.PrintAdminCommands();
                    //}
                    //else if (this.userSession.CurrentUser.UserType == UserType.User)
                    //{
                    //    this.writer.PrintUserCommands();
                    //}




                    //if (!input.ToLower().Equals("clear"))
                    //{
                    this.writer.WriteLine(processor.ProcessCommand(input));
                    //}
                    //else 
                    //{
                    //    this.writer.Clean();
                    //    this.writer.PrintHeader();
                    //    if (this.userSession.CurrentUser == null)
                    //    {
                    //        this.writer.PrintLoginCommands();
                    //    }
                    //    else if (this.userSession.CurrentUser.UserType == UserType.Admin)
                    //    {
                    //        this.writer.PrintAdminCommands();
                    //    }
                    //    else if (this.userSession.CurrentUser.UserType == UserType.User)
                    //    {
                    //        this.writer.PrintUserCommands();
                    //    }
                    //   }


                }


                catch (Exception ex)
                {
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }
                    this.writer.WriteLine(ex.Message);
                    this.exceptionLogging.SendErrorToText(ex);
                }
            }

        }


    }
}
