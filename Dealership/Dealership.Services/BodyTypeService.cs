using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using System;
using System.Linq;

namespace Dealership.Services
{
    public class BodyTypeService : IBodyTypeService
    {
        private readonly DealershipContext context;

        public BodyTypeService(DealershipContext context)
        {
            this.context = context;
        }

        public BodyType GetBodyType(string bodyName)
        {
            var bodyType = this.context.Chassis.FirstOrDefault(b => b.Name.ToLower() == bodyName);
            if (bodyType == null)
            {
                throw new InvalidOperationException($"There is no body type with name {bodyName}.");
            }
            return bodyType;
        }
    }
}
