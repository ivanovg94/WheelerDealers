using Autofac;
using Dealership.Client.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Dealership.Client
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Supported commands:");
            Console.WriteLine("add {brand} {model} {horse power} {engine capacity} {production date} {price} {chasis} {color} {color type} {fuel type} {gearbox} {number of gears}");
            Console.WriteLine("remove {carId}");
            Console.WriteLine("createextra {extra name}");
            Console.WriteLine("addextratocar {carId, extra name}");
            Console.WriteLine("getextrasforcar {carId}");
            Console.WriteLine("remove {carId}");

            Console.WriteLine("list active/sold/all asc/desc ");
            Console.WriteLine("filter: ");
            Console.WriteLine("     filterBrand {brandName}");
            Console.WriteLine("     filterYears {yearFrom} {yearTo}");

            Console.WriteLine("view {carId}");
            Console.WriteLine("edit  [id] [exact property] [new value]");
            Console.WriteLine("export sold? asc/desc");
            Console.WriteLine("import {filename} -ex: import cars");
            Console.WriteLine("generatePdf - output folder: DataProcessor/PdfReports");


            Console.WriteLine();
            Console.WriteLine();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            var engine = container.Resolve<IEngine>();
            engine.Run();
        }
    }
}
