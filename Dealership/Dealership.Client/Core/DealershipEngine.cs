using Dealership.Client.Core.Abstract;
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
        private readonly IRenderer renderer;
        private readonly IUserSession userSession;

        public DealershipEngine(IReader reader, IWriter writer, IUserSession userSession,
            IExceptionLogging exceptionLogging, ICommandProcessor commandProcessor, IRenderer renderer)
        {
            this.reader = reader;
            this.writer = writer;
            this.renderer = renderer;
            this.exceptionLogging = exceptionLogging;
            this.processor = commandProcessor;
            this.userSession = userSession;
        }

        string input = string.Empty;

        public void Run()
        {
            this.writer.WriteLine(this.renderer.GetCommandsInfo());
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
                    this.writer.WriteLine(processor.ProcessCommand(input));
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
