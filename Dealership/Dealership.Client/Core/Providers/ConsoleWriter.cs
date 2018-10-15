using Dealership.Client.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Core.Providers
{
    public class ConsoleWriter : IWriter
    {
        public void Write(string message)
        {
            Console.Write(message);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void PrintCommands()
        {
            Console.WriteLine("Supported commands:");

            Console.WriteLine("register {username} {password} {confirmPass} {email}");
            Console.WriteLine("login {username} {password}");
            Console.WriteLine("logout");
            Console.WriteLine("deleteUser {username} {password}");
            Console.WriteLine();

            Console.WriteLine("addcar {brand} {model} {horse power} {engine capacity} {production date} {price} {chasis} {color} {color type} {fuel type} {gearbox} {number of gears}");
            Console.WriteLine("removecar {carId}");
            Console.WriteLine("createextra {extra name}");
            Console.WriteLine("addextratocar {carId, extra name}");
            Console.WriteLine("getextrasforcar {carId}");
            Console.WriteLine("list active/sold/all asc/desc ");
            Console.WriteLine("filterbybrand {brandName} / filterbyyears {yearFrom} {yearTo}");
            Console.WriteLine("filterByPrice {priceFrom} {priceTo} / filterByBodyType {bodyType}");

            Console.WriteLine("viewcardetails {carId}");
            Console.WriteLine("edit [exact property] [id] [new value]");
            Console.WriteLine("export sold? asc/desc");
            Console.WriteLine("import {filename} -ex: import cars");
            Console.WriteLine("generatePdf - output folder: DataProcessor/PdfReports");

            Console.WriteLine("addCarToFavorites {carId}");
            Console.WriteLine("removeCarFromFavorites {carId}");
            Console.WriteLine("listFavorites");
        
            Console.WriteLine();
            Console.WriteLine();


        }

        //public void PrintAdminCommands()
        //{
        //    Console.WriteLine("logout");
        //    Console.WriteLine("deleteUser {username} {password}");
        //    Console.WriteLine();

        //    Console.WriteLine("addcar {brand} {model} {horse power} {engine capacity} {production date} {price} {chasis} {color} {color type} {fuel type} {gearbox} {number of gears}");
        //    Console.WriteLine("removecar {carId}");
        //    Console.WriteLine("createextra {extra name}");
        //    Console.WriteLine("addextratocar {carId, extra name}");
        //    Console.WriteLine("getextrasforcar {carId}");
        //    Console.WriteLine("edit [exact property] [id] [new value]");
        //    Console.WriteLine("list active/sold/all asc/desc ");
        //    Console.WriteLine("filterbybrand {brandName} / filterbyyears {yearFrom} {yearTo}");
        //    Console.WriteLine("filterByPrice {priceFrom} {priceTo} / filterByBodyType {bodyType}");
        //    Console.WriteLine("viewcardetails {carId}");
        //    Console.WriteLine("export sold? asc/desc");
        //    Console.WriteLine("generatePdf - output folder: DataProcessor/PdfReports");
        //    Console.WriteLine("import {filename} -ex: import cars");

        //    Console.WriteLine();
        //    Console.WriteLine();
        //}

        //public void PrintLoginCommands()
        //{

        //    Console.WriteLine("register {username} {password} {confirmPass} {email}");
        //    Console.WriteLine("login {username} {password}");
        //    Console.WriteLine("logout");


        //}
        //public void PrintUserCommands()
        //{
        //    Console.WriteLine("logout");
        //    Console.WriteLine("deleteUser {username} {password} (current user only)");
        //    Console.WriteLine();

        //    Console.WriteLine("list active/sold/all asc/desc ");
        //    Console.WriteLine("filterbybrand {brandName} / filterbyyears {yearFrom} {yearTo}");
        //    Console.WriteLine("filterByPrice {priceFrom} {priceTo} / filterByBodyType {bodyType}");
        //    Console.WriteLine("viewcardetails {carId}");
        //    Console.WriteLine("export sold? asc/desc");
        //    Console.WriteLine("generatePdf - output folder: DataProcessor/PdfReports");
        //    Console.WriteLine();
        //    Console.WriteLine();
        //}

        //public void PrintHeader()
        //{
        //    Console.WriteLine("Supported commands:");
        //}

        //public void Clean()
        //{
        //    Console.Clear();
        //}
    }
}
