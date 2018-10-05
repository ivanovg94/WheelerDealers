using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Dealership.Client.Commands;
using Dealership.Client.Core;
using Dealership.Client.Core.Abstract;

namespace Dealership.Client.Module
{
    public class DealershipModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DealershipEngine>().As<IEngine>().SingleInstance();
            builder.RegisterType<AddCommand>().Named<ICommand>("create");
            builder.RegisterType<RemoveCommand>().Named<ICommand>("remove");
            builder.RegisterType<ListCommand>().Named<ICommand>("list");

            base.Load(builder);
        }
    }
}
