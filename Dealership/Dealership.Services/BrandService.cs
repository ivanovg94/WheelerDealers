using Dealership.Data.Models;
using Dealership.Data.UnitOfWork;
using Dealership.Services.Abstract;
using Dealership.Services.Exceptions;
using System.Linq;

namespace Dealership.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork unitOfWork;

        public BrandService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Brand GetBrand(string brandName)
        {
            var brand = this.unitOfWork.GetRepository<Brand>().All().FirstOrDefault(b => b.Name == brandName);
            if (brand == null)
            {
                throw new BrandNotFoundException($"There is no brand with name {brandName}.");
            }
            return brand;
        }
    }
}
