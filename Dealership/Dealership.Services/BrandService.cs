using Dealership.Data.Models;
using Dealership.Data.Repository;
using Dealership.Services.Abstract;
using Dealership.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Services
{
   public class BrandService : IBrandService
    {
        private readonly IRepository<Brand> brandRepository;

        public BrandService(IRepository<Brand> brandRepository)
        { 
            this.brandRepository = brandRepository;
        }

        public Brand GetBrand(string brandName)
        {
            var brand = this.brandRepository.All().FirstOrDefault(b => b.Name == brandName);

            if (brand == null)
            {
                throw new BrandNotFoundException($"There is no brand with name \"{brandName}\".");
            }

            return brand;
        }
    }
}
