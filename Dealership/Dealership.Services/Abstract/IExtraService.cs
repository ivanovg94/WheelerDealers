using Dealership.Data.Models;
using System.Collections.Generic;

namespace Dealership.Services.Abstract
{
    public interface IExtraService
    {
        Extra AddExtraToCar(int carId, string extraName);
        Extra CreateExtra(string name);
        ICollection<Extra> GetAllExtras();
        Extra GetExtraById(int id);
        Extra GetExtraByName(string name);
        ICollection<Extra> GetExtrasForCar(int carId);
    }
}