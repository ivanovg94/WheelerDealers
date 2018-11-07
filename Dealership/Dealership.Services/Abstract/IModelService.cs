using System.Collections.Generic;
using Dealership.Data.Models;

namespace Dealership.Services.Abstract
{
    public interface IModelService
    {
        ICollection<CarModel> GetAllModelsByBrandId(int brandId);
        CarModel GetModel(int id);
    }
}