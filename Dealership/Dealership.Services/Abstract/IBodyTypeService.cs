using Dealership.Data.Models;

namespace Dealership.Services.Abstract
{
    public interface IBodyTypeService
    {
        BodyType GetBodyType(string bodyName);
    }
}
