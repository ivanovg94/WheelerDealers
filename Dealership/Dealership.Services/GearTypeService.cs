using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Services
{
    public class GearTypeService : IGearTypeService
    {
        private readonly DealershipContext context;

        public GearTypeService(DealershipContext context)
        {
            this.context = context;
        }

        public IList<GearType> GetGearTypes()
        {
            return this.context.GearTypes.ToList();
        }

        public IList<Gearbox> GetGearboxesDependingOnGearType(int id)
        {
            return this.context.Gearboxes.Where(g => g.GearTypeId == id).ToList();
        }

        public IList<GearType> GetNumberOfGearsTypes()
        {
            return this.context.GearTypes.ToList();
        }
    }
}
