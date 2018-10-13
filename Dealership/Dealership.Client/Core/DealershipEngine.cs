using Dealership.Client.Core.Abstract;
using System;

namespace Dealership.Client.Core
{
    public class DealershipEngine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IExceptionLogging exceptionLogging;
        private readonly ICommandProcessor processor;

        public DealershipEngine(IReader reader, IWriter writer, IExceptionLogging exceptionLogging, ICommandProcessor commandProcessor)
        {
            this.reader = reader;
            this.writer = writer;
            this.exceptionLogging = exceptionLogging;
            this.processor = commandProcessor;
        }

        string input = string.Empty;

        public void Run()
        {
            while ((input = reader.ReadLine()) != "exit")
            {
                try
                {
                    writer.WriteLine(processor.ProcessCommand(input));
                }
                catch (Exception ex)
                {
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }
                    writer.WriteLine(ex.Message);
                    this.exceptionLogging.SendErrorToText(ex);
                }
            }

        }


    }
}
