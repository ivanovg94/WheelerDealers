using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Services
{
    public class ExtraService : IExtraService
    {
        public IDealershipContext Context { get; set; }
        public ICarService CarService { get; set; }

        public Extra CreateExtra(string name)
        {
            var extra = new Extra() { Name = name };
            this.Context.Extras.Add(extra);
            this.Context.SaveChanges();
            return extra;
        }

        public Extra AddExtraToCar(int carId, string extraName)
        {
            if (this.Context.Cars.Any(c => c.Id == carId))
            {
                throw new ArgumentException($"Car with Id {carId} does not exist");
            }

            if (this.Context.Cars.Include(c => c.CarsExtras)
                                   .ThenInclude(ce => ce.Extra)
                                 .FirstOrDefault(c => c.Id == carId)
                                 .CarsExtras.Any(ce => ce.Extra.Name == extraName))
            {
                throw new ArgumentException($"Car with Id {carId} already has extra with name {extraName}.");
            }

            //TODO: validate
            Extra extra = null;
            if (!this.Context.Extras.Any(e => e.Name == extraName))
            {
                extra = new Extra() { Name = extraName };
                this.Context.Extras.Add(extra);
                this.Context.SaveChanges();
            }
            else
            {
                extra = GetExtraByName(extraName);
            }

            var newCarExtra = new CarsExtras() { CarId = carId, ExtraId = extra.Id };
            this.Context.CarsExtras.Add(newCarExtra);
            //       extra.CarsExtras.Add(newCarExtra);

            this.Context.SaveChanges();
            return extra;
        }

        public Extra GetExtraById(int id)
        {
            var extra = Context.Extras.FirstOrDefault();
            return extra;

        }

        public Extra GetExtraByName(string name)
        {
            return this.Context.Extras.FirstOrDefault(e => e.Name == name);
        }

        public ICollection<Extra> GetAllExtras()
        {
            return this.Context.Extras.ToList();
        }

        public ICollection<Extra> GetExtrasForCar(int carId)
        {
            if (!this.Context.Cars.Any(c => c.Id == carId))
            {
                throw new ArgumentException("Invalid car Id.");
            }
            return this.Context.Cars.Include(c => c.CarsExtras)
                                        .ThenInclude(ce => ce.Extra)
                                        .First(c => c.Id == carId).CarsExtras
                                        .Select(x => x.Extra).ToList();
        }

    }
}
