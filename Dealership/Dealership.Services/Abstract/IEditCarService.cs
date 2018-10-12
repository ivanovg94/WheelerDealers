using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Services.Abstract
{
    public interface IEditCarService
    {
        void EditBrand(string[] parameters);

        void EditModel(string[] parameters);

        void EditHorsePower(string[] parameters);

        void EditEngineCapacity(string[] parameters);

        void EditIsSold(string[] parameters);

        void EditPrice(string[] parameters);

        void EditProductionDate(string[] parameters);

        void EditBodyType(string[] parameters);

        void EditColor(string[] parameters);

        void EditColorType(string[] parameters);

        void EditFuelType(string[] parameters);

        void EditGearbox(string[] parameters);
    }
}
