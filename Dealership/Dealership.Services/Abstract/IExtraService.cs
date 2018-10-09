using System.Collections.Generic;
using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;

namespace Dealership.Services.Abstract
{
    public interface IExtraService
    {
        ICarService CarService { get; set; }
        IDealershipContext Context { get; set; }

        Extra AddExtraToCar(int carId, string extraName);
        Extra CreateExtra(string name);
        ICollection<Extra> GetAllExtras();
        Extra GetExtraById(int id);
        Extra GetExtraByName(string name);
        ICollection<Extra> GetExtrasForCar(int carId);
    }
}