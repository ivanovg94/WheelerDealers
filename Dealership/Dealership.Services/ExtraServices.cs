using Dealership.Data.Context;
using Dealership.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Services
{
    public class ExtraServices
    {
        public IDealershipContext Context { get; set; }

        public Extra GetExtraById(int id)
        {
            return Context.Extras.FirstOrDefault();
        }

        public Extra GetExtraByName(string name)
        {
            throw new NotImplementedException();
        }

        public ICollection<Extra> GetAllExtras()
        {
            throw new NotImplementedException();

        }

        public ICollection<Extra> GetExtrasForCar(int carId)
        {
            throw new NotImplementedException();
        }

    }
}
