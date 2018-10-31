using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Services
{
    public class FuelTypeService : IFuelTypeService
    {
        private readonly DealershipContext context;

        public FuelTypeService(DealershipContext context)
        {
            this.context = context;
        }

        public IList<FuelType> GetFuelTypes()
        {
            return this.context.FuelTypes.ToList();
        }
    }
}
