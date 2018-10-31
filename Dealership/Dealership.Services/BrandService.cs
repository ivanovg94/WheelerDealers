using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Dealership.Services.Exceptions;
using System.Linq;

namespace Dealership.Services
{
    public class BrandService : IBrandService
    {
        private readonly DealershipContext context;

        public BrandService(DealershipContext context)
        {
            this.context = context;
        }

        public Brand GetBrand(string brandName)
        {
            var brand = this.context.Brands.FirstOrDefault(b => b.Name == brandName);
            if (brand == null)
            {
                throw new ServiceException($"There is no brand with name {brandName}.");
            }
            return brand;
        }
    }
}
