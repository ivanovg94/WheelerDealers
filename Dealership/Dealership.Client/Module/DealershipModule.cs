using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Dealership.Client.Commands;
using Dealership.Client.Commands.CRUD;
using Dealership.Client.Commands.CRUD.EditCommands;
using Dealership.Client.Contracts;
using Dealership.Client.Contracts.Abstract;
using Dealership.Client.Core;
using Dealership.Client.Core.Abstract;
using Dealership.Data.Context;
using Dealership.Services;
using Dealership.Services.Abstract;

namespace Dealership.Client.Module
{
    public class DealershipModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DealershipEngine>().As<IEngine>().SingleInstance();
            builder.RegisterType<DealershipContext>().As<IDealershipContext>().SingleInstance();
            builder.RegisterType<CarService>().As<ICarService>().SingleInstance();


            builder.RegisterType<AddCarCommand>().Named<ICommand>("add").PropertiesAutowired();
            builder.RegisterType<RemoveCarCommand>().Named<ICommand>("remove").PropertiesAutowired();
            builder.RegisterType<ListCommand>().Named<ICommand>("list").PropertiesAutowired();
            builder.RegisterType<EditCommand>().Named<ICommand>("edit").PropertiesAutowired();
            builder.RegisterType<ExportCommand>().Named<ICommand>("export").PropertiesAutowired();
            builder.RegisterType<ImportCommand>().Named<ICommand>("import").PropertiesAutowired();
            builder.RegisterType<FilterByBrandCommand>().Named<ICommand>("filterBrand").PropertiesAutowired();
            builder.RegisterType<ViewCarDetailsCommand>().Named<ICommand>("view").PropertiesAutowired();
            builder.RegisterType<EditBrandCommand>().Named<ICommand>("editbrand").PropertiesAutowired();
            builder.RegisterType<EditModelCommand>().Named<ICommand>("editmodel").PropertiesAutowired();
            builder.RegisterType<EditChassisCommand>().Named<ICommand>("editchassis").PropertiesAutowired();
            builder.RegisterType<EditColorCommand>().Named<ICommand>("editcolor").PropertiesAutowired();
            builder.RegisterType<EditEngineCapacity>().Named<ICommand>("editenginecapacity").PropertiesAutowired();
            builder.RegisterType<EditFuelTypeCommand>().Named<ICommand>("editfueltype").PropertiesAutowired();
            builder.RegisterType<EditGearbox>().Named<ICommand>("editgearbox").PropertiesAutowired();
            builder.RegisterType<EditHorsePowerCommand>().Named<ICommand>("edithorsepower").PropertiesAutowired();
            builder.RegisterType<EditPriceCommand>().Named<ICommand>("editprice").PropertiesAutowired();
            builder.RegisterType<EditProductionDateCommand>().Named<ICommand>("editdate").PropertiesAutowired();
            
            base.Load(builder);
        }
    }
}
