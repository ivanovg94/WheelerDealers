using Dealership.Client.Commands.Abstract;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Linq;

namespace Dealership.Client.Commands
{
    public class GeneratePdfCommand : Command
    {
        const string outputDir = @"..\..\..\..\Dealership.Data\DataProcessor\PdfReports\";
        private readonly ICarService carService;

        public GeneratePdfCommand(IUserSession userSession, ICarService carService) : base(userSession)
        {
            this.carService = carService;
        }


        public override string Execute(string[] parameters)
        {
            var exportFile = outputDir + "Report.pdf";

            var cars = this.carService.GetCars(false, "asc").ToList();

            using (var pdfWriter = new PdfWriter(exportFile))
            {
                using (var pdf = new PdfDocument(pdfWriter))
                {
                    var doc = new Document(pdf);
                    var table = new Table(10);

                    table.SetTextAlignment(TextAlignment.CENTER);
                    table.SetBold();

                    var cell = new Cell(1, 10).Add(new Paragraph("Wheeler Dealer's available cars"));

                    table.AddCell(cell);

                    table.AddCell("Brand");
                    table.AddCell("Model");
                    table.AddCell("Year");
                    table.AddCell("Horsepower");
                    table.AddCell("Capacity");
                    table.AddCell("Body Type");
                    table.AddCell("Color");
                    table.AddCell("Gearbox");
                    table.AddCell("Fuel");
                    table.AddCell("Price");

                    foreach (var car in cars)
                    {
                        table.AddCell(car.Brand.Name);
                        table.AddCell(car.Model);
                        table.AddCell(car.ProductionDate.Year.ToString());
                        table.AddCell(car.HorsePower.ToString());
                        table.AddCell(car.EngineCapacity.ToString());
                        table.AddCell(car.BodyType.Name);
                        table.AddCell(car.Color.Name);
                        table.AddCell(car.GearBox.GearType.Name);
                        table.AddCell(car.FuelType.Name);
                        table.AddCell(car.Price.ToString());
                    }

                    cell = new Cell(1, 10).Add(new Paragraph($"Total cars: {cars.Count}"));
                    table.AddCell(cell);

                    doc.Add(table);
                }
            }

            return "PDF generated successfully.";
        }
    }
}
