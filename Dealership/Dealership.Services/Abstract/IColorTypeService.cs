using System.Collections.Generic;
using Dealership.Data.Models;

namespace Dealership.Services.Abstract
{
    public interface IColorTypeService
    {
        IList<ColorType> GetColorTypes();
        ColorType GetColorType(int id);

    }
}