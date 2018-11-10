using Dealership.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dealership.Services.Abstract
{
    public interface IBodyTypeService
    {
        BodyType GetBodyType(string bodyName);
        Task<BodyType> GetBodyType(int id);


        IList<BodyType> GetBodyTypes();
    }
}
