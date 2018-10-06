using Dealership.Client.Commands.Abstract;
using Dealership.Client.Core.Abstract;
using Dealership.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Client.Commands.CRUD
{
    public class ListCommand : PrimeCommand
    {
        public ListCommand()
        {
        }

        //list | null/sold direction
        public override string Execute(string[] parameters)
        {
            ////Projection Loading
            //var querry = this.ShopContext.Products
            //    .Select(p => new
            //    {
            //        Name = p.ProductName,
            //        Price = p.Price,
            //        CategoryName = p.Category.CategoryName,
            //        TagNames = p.ProductsTags.Select(pt => pt.Tag.TagName).ToList()
            //    });

            //    bool isSold = false;
            //    if (parameters.Length == 2) { isSold = false; }

            //    //BMW 330 258 2998 2005-01-01 14999 coupe Black Metalic Gasoline Manual 6
            //    base.Context.Cars.Where(c => c.IsSold == isSold)
            //                     .Select(c => new
            //                     {
            //                         Brand = c.Brand.Name,
            //                         Model = c.Model,
            //                         EngineCap = c.EngineCapacity,
            //                         ProductionDate = c.ProductionDate,
            //                         Price = c.Price,
            //                         Chasis = c.Chasis.Name,
            //                         Color = c.Color.Name,
            //                         ColorType = c.Color.ColorType.Type,
            //                         Fuel = c.FuelType.Type,
            //                         Gearbox = c.GearBox.GearType.Type,
            //                         NumberOfGears = c.GearBox.NumberOfGears,
            //                         Extras = c.CarsExtras.Select(ce => ce.Extra.Name).ToList()
            //                     })
            return "";
        }
    }
}
