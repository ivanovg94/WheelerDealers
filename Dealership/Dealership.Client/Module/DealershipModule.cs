﻿using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Dealership.Client.Commands;
using Dealership.Client.Contracts;
using Dealership.Client.Core;
using Dealership.Client.Core.Abstract;
using Dealership.Data.Context;

namespace Dealership.Client.Module
{
    public class DealershipModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DealershipEngine>().As<IEngine>().SingleInstance();
            builder.RegisterType<DealershipContext>().As<IDealershipContext>().SingleInstance();

            builder.RegisterType<AddCarCommand>().Named<ICommand>("add").PropertiesAutowired();
            builder.RegisterType<RemoveCommand>().Named<ICommand>("remove").PropertiesAutowired();
            builder.RegisterType<ListCommand>().Named<ICommand>("list").PropertiesAutowired();
            builder.RegisterType<EditCommand>().Named<ICommand>("edit").PropertiesAutowired();
            builder.RegisterType<ExportCommand>().Named<ICommand>("export").PropertiesAutowired();
            builder.RegisterType<ImportCommand>().Named<ICommand>("import").PropertiesAutowired();
            builder.RegisterType<FilterCommand>().Named<ICommand>("filter").PropertiesAutowired();
            builder.RegisterType<ViewCarDetailsCommand>().Named<ICommand>("view").PropertiesAutowired();


            base.Load(builder);
        }
    }
}
