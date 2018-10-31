using System.Collections.Generic;
using Dealership.Data.Models;

namespace Dealership.Services.Abstract
{
    public interface IGearTypeService
    {
        IList<GearType> GetGearTypes();
    }
}