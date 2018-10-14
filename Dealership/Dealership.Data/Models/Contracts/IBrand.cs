using Dealership.Data.Models.Contracts;
using System.Collections.Generic;

namespace Dealership.Data.Models.Contracts
{
    public interface IBrand: IDeletable
    {
        ICollection<Car> Cars { get; set; }
        string Name { get; set; }
    }
}