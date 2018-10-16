using Dealership.Client.Core.Abstract;
using System.Text;

namespace Dealership.Client.Core.Providers
{
    public class ConsoleRenderer : IRenderer
    {
        public string GetCommandsInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Supported commands:");
            sb.AppendLine();
            sb.AppendLine("register {username} {password} {confirmPass} {email}");
            sb.AppendLine("login {username} {password}");
            sb.AppendLine("logout");
            sb.AppendLine("deleteUser {username} {password}");
            sb.AppendLine();
            sb.AppendLine("addcar {brand} {model} {horse power} {engine capacity} {production date} {price} {chasis} {color} {color type} {fuel type} {gearbox} {number of gears}");
            sb.AppendLine("removecar {carId}");
            sb.AppendLine("createextra {extra name}");
            sb.AppendLine("addextratocar {carId, extra name}");
            sb.AppendLine("getextrasforcar {carId}");
            sb.AppendLine("list active/sold/all asc/desc ");
            sb.AppendLine("filterbybrand {brandName} / filterbyyears {yearFrom} {yearTo}");
            sb.AppendLine("filterByPrice {priceFrom} {priceTo} / filterByBodyType {bodyType}");
            sb.AppendLine();
            sb.AppendLine("viewcardetails {carId}");
            sb.AppendLine("edit [exact property] [id] [new value]");
            sb.AppendLine("export sold? asc/desc");
            sb.AppendLine("import {filename} -ex: import cars");
            sb.AppendLine("generatePdf - output folder: DataProcessor/PdfReports");
            sb.AppendLine();
            sb.AppendLine("addCarToFavorites {carId}");
            sb.AppendLine("removeCarFromFavorites {carId}");
            sb.AppendLine("listFavorites");
            sb.AppendLine();

            return sb.ToString();
        }
    }
}
