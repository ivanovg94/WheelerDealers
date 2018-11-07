using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Services
{
    public class ModelService : IModelService
    {
        private readonly DealershipContext context;
        private readonly IBrandService brandService;

        public ModelService(DealershipContext context, IBrandService brandService)
        {
            this.context = context;
            this.brandService = brandService;
        }

        public ICollection<CarModel> GetAllModelsByBrandId(int brandId)
        {
            return this.brandService.GetBrand(brandId).CarModels;
        }

        public CarModel GetModel(int id)
        {
            return this.context.CarModels.FirstOrDefault(m => m.Id == id);
        }

    }
}
