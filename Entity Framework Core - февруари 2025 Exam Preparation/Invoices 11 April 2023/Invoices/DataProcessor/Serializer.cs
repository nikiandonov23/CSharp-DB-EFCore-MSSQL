using System.Globalization;
using Invoices.Data.Models.Enums;
using Invoices.DataProcessor.ExportDto;
using Invoices.Utilities;

namespace Invoices.DataProcessor
{
    using Invoices.Data;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportClientsWithTheirInvoices(InvoicesContext context, DateTime date)
        {
            const string xmlRoot = "Clients";

            var validClients = context.Clients
                .Where(x => x.Invoices.Any(x => x.IssueDate > date))
                .OrderByDescending(x=>x.Invoices.Count)
                .ThenBy(x=>x.Name)
                .Select(x => new ExportClientInvoicesDto()
                {
                    InvoicesCount = x.Invoices.Count,
                    ClientName = x.Name,
                    VatNumber = x.NumberVat,
                    Invoices = x.Invoices
                        . OrderBy(x => x.IssueDate)
                        .ThenByDescending(x=>x.DueDate)
                        .Select(i => new ExportInvoicesDto()
                        {
                            InvoiceNumber = i.Number.ToString(),
                            InvoiceAmount = i.Amount.ToString("0.##"),
                            DueDate = i.DueDate.ToString("MM/dd/yyyy",CultureInfo.InvariantCulture),
                            Currency = i.CurrencyType.ToString()
                        }).ToArray()


                }).ToArray();

            string result = XmlHelper.Serialize(validClients, xmlRoot);
            return result;
        }

        public static string ExportProductsWithMostClients(InvoicesContext context, int nameLength)
        {

            var validProducts = context.Products
                .Where(x => x.ProductsClients.Any(x => x.Client.Name.Length >= nameLength))
                .Select(x => new ExportProductClientsDto()
                {
                    Name = x.Name,
                    Price = decimal.Parse(x.Price.ToString("0.##")),
                    Category = x.CategoryType.ToString(),
                    Clients = x.ProductsClients
                        .Where(x => x.Client.Name.Length >= nameLength)
                        .Select(x => new ExportClientsDto()
                        {
                            Name = x.Client.Name,
                            NumberVat = x.Client.NumberVat

                        })
                        .OrderBy(x => x.Name)
                        .ToArray()

                })
                .OrderByDescending(x => x.Clients.Length)
                .ThenBy(x=>x.Name)
                .ToArray()
                .Take(5);

            string result = JsonConvert.SerializeObject(validProducts, Formatting.Indented);
            return result;

        }
    }
}