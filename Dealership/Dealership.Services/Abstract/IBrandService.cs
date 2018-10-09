using Dealership.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Services.Abstract
{
    public interface IBrandService
    {
        Brand GetBrand(string brandName);
    }
}
