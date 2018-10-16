using Dealership.Data.Models;
using Dealership.Data.UnitOfWork;
using Dealership.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Services
{
    public class ExtraService : IExtraService
    {
        private readonly IUnitOfWork unitOfWork;

        public ExtraService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Extra CreateExtra(string name)
        {
            if (this.unitOfWork.GetRepository<Extra>().All().Any(e => e.Name == name))
            {
                throw new ArgumentException($"An extra with name {name} already exists!");
            }

            var extra = new Extra() { Name = name };
            this.unitOfWork.GetRepository<Extra>().Add(extra);
            this.unitOfWork.SaveChanges();
            return extra;
        }

        public Extra AddExtraToCar(int carId, string extraName)
        {
            if (!this.unitOfWork.GetRepository<Car>().All().Any(c => c.Id == carId))
            {
                throw new ArgumentException($"Car with Id {carId} does not exist");
            }

            if (this.unitOfWork.GetRepository<Car>().All()
                                 .Include(c => c.CarsExtras)
                                   .ThenInclude(ce => ce.Extra)
                                 .FirstOrDefault(c => c.Id == carId)
                                 .CarsExtras.Any(ce => ce.Extra.Name == extraName))
            {
                throw new ArgumentException($"Car with Id {carId} already has extra with name {extraName}.");
            }

            var extra = GetExtraByName(extraName);
            if (extra == null)
            {
                extra = new Extra() { Name = extraName };
                this.unitOfWork.GetRepository<Extra>().Add(extra);
                this.unitOfWork.SaveChanges();
            }

            var newCarExtra = new CarsExtras() { CarId = carId, ExtraId = extra.Id };
            this.unitOfWork.GetRepository<CarsExtras>().Add(newCarExtra);

            this.unitOfWork.SaveChanges();
            return extra;
        }

        public Extra GetExtraById(int id)
        {
            return this.unitOfWork.GetRepository<Extra>().All().FirstOrDefault(x => x.Id == id);
        }

        public Extra GetExtraByName(string name)
        {
            return this.unitOfWork.GetRepository<Extra>().All().FirstOrDefault(e => e.Name == name);
        }

        public ICollection<Extra> GetAllExtras()
        {
            return this.unitOfWork.GetRepository<Extra>().All().ToList();
        }

        public ICollection<Extra> GetExtrasForCar(int carId)
        {
            if (!this.unitOfWork.GetRepository<Car>().All().Any(c => c.Id == carId))
            {
                throw new ArgumentException("Invalid car Id.");
            }
            return this.unitOfWork.GetRepository<Car>().All()
                                        .Include(c => c.CarsExtras)
                                        .ThenInclude(ce => ce.Extra)
                                        .First(c => c.Id == carId).CarsExtras
                                        .Select(x => x.Extra).ToList();
        }
    }
}
