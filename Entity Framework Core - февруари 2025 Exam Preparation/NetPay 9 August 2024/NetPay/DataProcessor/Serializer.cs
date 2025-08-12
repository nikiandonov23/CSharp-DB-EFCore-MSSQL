using Microsoft.EntityFrameworkCore;
using NetPay.Data;
using NetPay.Data.Models;
using NetPay.Data.Models.Enums;
using NetPay.DataProcessor.ExportDtos;
using NetPay.Utilities;
using Newtonsoft.Json;

namespace NetPay.DataProcessor
{
    public class Serializer
    {
        public static string ExportHouseholdsWhichHaveExpensesToPay(NetPayContext context)
        {

            const string xmlRoot = "Households";

            var validHouseholds = context.Households
                .Include(x => x.Expenses)
                .ThenInclude(x => x.Service)
                .ToArray()
                .Where(x => x.Expenses.Any(x => x.PaymentStatus != PaymentStatus.Paid))

                .Select(x => new ExportHouseholdExpencesXmlDto()
                {
                    ContactPerson = x.ContactPerson,
                    Email = x.Email, //MOJE DA E NULL SHTOTO NE E REQUIRED V DTO-to
                    PhoneNumber = x.PhoneNumber,
                    Expenses = x.Expenses
                        .Where(x => x.PaymentStatus != PaymentStatus.Paid)
                        .Select(x => new ExportExpencesXmlDto()
                        {
                            ExpenseName = x.ExpenseName,
                            Amount = x.Amount.ToString("F2"),
                            PaymentDate = x.DueDate.ToString("yyyy-MM-dd"),
                            ServiceName = x.Service.ServiceName
                        })
                        .OrderBy(x => x.PaymentDate)
                        .ThenBy(x => x.Amount)

                        .ToArray()


                }).OrderBy(x => x.ContactPerson)
                .ToArray();

            string result = XmlHelper.Serialize(validHouseholds, xmlRoot);
            return result;
        }

        public static string ExportAllServicesWithSuppliers(NetPayContext context)
        {
            var validServices = context.Services
                .Select(x => new
                {
                    ServiceName = x.ServiceName,
                    Suppliers = x.SuppliersServices
                        .Select(x => new
                        {
                            SupplierName = x.Supplier.SupplierName
                        })
                        .OrderBy(x=>x.SupplierName)
                        .ToArray()


                })
                .OrderBy(x=>x.ServiceName)
                .ToArray();



            string result = JsonConvert.SerializeObject(validServices, Formatting.Indented);
            return result;
        }
    }
}
