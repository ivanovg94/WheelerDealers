using Dealership.Data.Models;
using Dealership.Data.UnitOfWork;
using Dealership.Services.Abstract;
using Dealership.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Services
{
    public class BodyTypeService : IBodyTypeService
    {
        private readonly IUnitOfWork unitOfWork;

        public BodyTypeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public BodyType GetBodyType(string bodyName)
        {
            var bodyType = this.unitOfWork.GetRepository<BodyType>().All().FirstOrDefault(b => b.Name.ToLower() == bodyName);
            if (bodyType == null)
            {
                throw new InvalidOperationException($"There is no body type with name \"{bodyName}\".");
            }
            return bodyType;
        }
    }
}
