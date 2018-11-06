using Dealership.Data.Models;

namespace Dealership.Services.Abstract
{
    public interface IBrandService
    {
        Brand GetBrand(string brandName);
        Brand Add(Brand newBrand);
        Brand Create(string brand);
    }
}
