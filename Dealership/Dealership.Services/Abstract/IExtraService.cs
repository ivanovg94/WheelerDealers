﻿using Dealership.Data.Models;
using System.Collections.Generic;

namespace Dealership.Services.Abstract
{
    public interface IExtraService
    {
        Extra AddExtraToCar(int carId, string extraName);

        Extra CreateExtra(string name);

        Extra GetExtraById(int id);

        Extra GetExtraByName(string name);

        Extra AddExtra(Extra extra);

        ICollection<Extra> GetAllExtras();

        ICollection<Extra> GetExtrasForCar(int carId);
    }
}