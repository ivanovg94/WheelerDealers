using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
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

        public CarsExtras AddExtraToCar(int carId, string extraName)
        {
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
            this.CarService.GetCar(carId).CarsExtras.Add(newCarExtra);
     //       extra.CarsExtras.Add(newCarExtra);

            this.Context.SaveChanges();
            return newCarExtra;
        }

        public Extra GetExtraById(int id)
        {
            return Context.Extras.FirstOrDefault();
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
            return this.CarService.GetCar(carId).CarsExtras.Select(x => x.Extra).ToList();
        }

    }
}
