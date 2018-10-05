using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Dealership.Client.Core;
using Dealership.Client.Core.Abstract;

namespace Dealership.Client.Module
{
    public class DealershipModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DealershipEngine>().As<IEngine>().SingleInstance();


            base.Load(builder);
        }
    }
}
