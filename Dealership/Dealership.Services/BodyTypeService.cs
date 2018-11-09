using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var bodyType = this.context.BodyTypes.FirstOrDefault(b => b.Name.ToLower() == bodyName);
            if (bodyType == null)
            {
                throw new InvalidOperationException($"There is no body type with name {bodyName}.");
            }
            return bodyType;
        }

        public async Task<BodyType> GetBodyType(int id)
        {
            var bodyType = await context.BodyTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (bodyType == null)
            {
                throw new InvalidOperationException($"There is no body type with id {id}.");
            }
            return bodyType;
        }

        public IList<BodyType> GetBodyTypes()
        {
            return this.context.BodyTypes.ToList();
        }
    }
}
