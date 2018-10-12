using Autofac;
using Dealership.Client.Contracts.Abstract;
using Dealership.Client.Core;
using Dealership.Client.Core.Abstract;
using Dealership.Client.Core.Providers;
using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using Dealership.Data.Repository;
using Dealership.Data.UnitOfWork;
using Dealership.Services;
using Dealership.Services.Abstract;
using System.Linq;
using System.Reflection;

namespace Dealership.Client.Module
{
    public class DealershipModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterCoreComponents(builder);
            RegisterDynamicCommands(builder);
            RegisterRepository(builder);

            base.Load(builder);
        }

        private void RegisterCoreComponents(ContainerBuilder builder)
        {
            builder.RegisterType<DealershipEngine>().As<IEngine>().SingleInstance();
            builder.RegisterType<DealershipContext>().As<IDealershipContext>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<CarService>().As<ICarService>();
            builder.RegisterType<EditCarService>().As<IEditCarService>();
            builder.RegisterType<ExtraService>().As<IExtraService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<BrandService>().As<IBrandService>();
            builder.RegisterType<ConsoleReader>().As<IReader>().SingleInstance();
            builder.RegisterType<ConsoleWriter>().As<IWriter>().SingleInstance();
            builder.RegisterType<UserSession>().As<IUserSession>().SingleInstance();
            builder.RegisterType<ExceptionLogging>().As<IExceptionLogging>().SingleInstance();
            builder.RegisterType<CommandParser>().As<ICommandParser>();
            builder.RegisterType<CommandProcessor>().As<ICommandProcessor>();
        }

        private void RegisterDynamicCommands(ContainerBuilder builder)
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            var commandTypes = currentAssembly
                               .DefinedTypes
                               .Where(typeInfo =>
                                    typeInfo.ImplementedInterfaces.Contains(typeof(ICommand))
                                    && typeInfo.IsAbstract == false)
                               .ToList();

            // register in autofac
            foreach (var commandType in commandTypes)
            {
                builder.RegisterType(commandType.AsType())
                                    .Named<ICommand>(commandType.Name.ToLower().Replace("command", ""))
                                    .PropertiesAutowired();
            }
        }

        private void RegisterRepository(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
        }
    }
}
