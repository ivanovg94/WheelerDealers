using Dealership.Client.Core.Abstract;
using System.Text;

namespace Dealership.Client.Core.Providers
{
    public class ConsoleRenderer : IRenderer
    {
        public string GetCommandsInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Supported commands (case insesitive):");
            sb.AppendLine("register {username} {password} {confirmPass} {email}");
            sb.AppendLine("login {username} {password}");
            sb.AppendLine("logout");
            sb.AppendLine("deleteUser {username} {password}");
            sb.AppendLine();
            sb.AppendLine("addCar {brand} {model} {horse power} {engine capacity} {production date} {price} {chasis} {color} {color type} {fuel type} {gearbox} {number of gears}");
            sb.AppendLine("removeCar {carId}");
            sb.AppendLine("createExtra {extra name}");
            sb.AppendLine("addExtraToCar {carId, extra name}");
            sb.AppendLine("getExtrasForCar {carId}");
            sb.AppendLine("list active/sold/all asc/desc ");
            sb.AppendLine("filterByBrand {brandName} / filterByBodyType {bodyType}");
            sb.AppendLine("filterByPrice {priceFrom} {priceTo} / filterByyears {yearFrom} {yearTo}");
            sb.AppendLine("viewCarDetails {carId}");
            sb.AppendLine("edit [exact property] [id] [new value]");
            sb.AppendLine("export asc/desc");
            sb.AppendLine("import {filename} - ex: import cars");
            sb.AppendLine("generatePdf - output folder: DataProcessor/PdfReports");
            sb.AppendLine("addCarToFavorites {carId}");
            sb.AppendLine("removeCarFromFavorites {carId}");
            sb.AppendLine("listFavorites");

            return sb.ToString();
        }
    }
}
