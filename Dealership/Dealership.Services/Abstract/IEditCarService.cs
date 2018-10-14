using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Services.Abstract
{
    public interface IEditCarService
    {
        string EditBrand(string[] parameters);
        
        string EditModel(string[] parameters);
        
        string EditHorsePower(string[] parameters);

        string EditEngineCapacity(string[] parameters);

        string EditIsSold(string[] parameters);

        string EditPrice(string[] parameters);

        string EditProductionDate(string[] parameters);

        string EditBodyType(string[] parameters);

        string EditColor(string[] parameters);

        string EditColorType(string[] parameters);

        string EditFuelType(string[] parameters);

        string EditGearbox(string[] parameters);
    }
}
