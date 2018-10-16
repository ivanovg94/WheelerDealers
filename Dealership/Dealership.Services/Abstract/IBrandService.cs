using Dealership.Data.Models;

namespace Dealership.Services.Abstract
{
    public interface IBrandService
    {
        Brand GetBrand(string brandName);
    }
}
