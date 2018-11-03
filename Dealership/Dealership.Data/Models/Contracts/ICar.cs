using Dealership.Data.Models.Contracts;
using System;
using System.Collections.Generic;

namespace Dealership.Data.Models.Contracts
{
    public interface ICar : IDeletable
    {
        int Id { get; set; }
        BodyType BodyType { get; set; }
        int BodyTypeId { get; set; }
        Brand Brand { get; set; }
        int BrandId { get; set; }
        IEnumerable<CarsExtras> CarsExtras { get; set; }
        Color Color { get; set; }
        int ColorId { get; set; }
        short EngineCapacity { get; set; }
        FuelType FuelType { get; set; }
        int FuelTypeId { get; set; }
        Gearbox GearBox { get; set; }
        int GearBoxId { get; set; }
        short HorsePower { get; set; }
        bool IsSold { get; set; }
        string Model { get; set; }
        decimal Price { get; set; }
        DateTime ProductionDate { get; set; }
    }
}