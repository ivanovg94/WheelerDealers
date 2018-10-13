﻿using Autofac;
using Dealership.Client.Contracts.Abstract;
using Dealership.Client.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Core.Providers
{
    public class CommandParser : ICommandParser
    {
        private IComponentContext containerContext;

        public CommandParser(IComponentContext containerContext)
        {
            this.containerContext = containerContext;
        }

        public ICommand ParseCommand(string args)
        {
            try
            {
                return this.containerContext.ResolveNamed<ICommand>(args);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("The entered command is invalid!");
            }
        }
    }
}