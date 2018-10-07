using Dealership.Data.Context;
using Dealership.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Services
{
    public class CarService : ICarService
    {
        private IDealershipContext context;

        public CarService(IDealershipContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Car AddCar( /*parameters*/ )
        {
            //logic
            return null;
        }

        public IEnumerable<Car> GetCars(bool filterSold, string direction)
        {
            var querry = this.context.Cars.Where(c => c.IsSold == filterSold);

            if (direction.ToLower() == "asc")
            {
                return querry.OrderBy(c => c.Id).ToList();
            }
            else if (direction.ToLower() == "desc")
            {
                return querry.OrderByDescending(c => c.Id).ToList();
            }
            else { throw new InvalidOperationException("Invalid direction!"); }
        }

        public Car GetCar( /*parameters*/ )
        {
            //logic
            throw new NotImplementedException();
        }
    }
}
