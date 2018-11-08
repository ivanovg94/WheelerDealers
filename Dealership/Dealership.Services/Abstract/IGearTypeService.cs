using System.Collections.Generic;
using Dealership.Data.Models;

namespace Dealership.Services.Abstract
{
    public interface IGearTypeService
    {
        Gearbox GetGearBox(int typeId, int numberOfGears);
        IList<GearType> GetGearTypes();
        IList<Gearbox> GetGearboxesDependingOnGearType(int id);

    }
}