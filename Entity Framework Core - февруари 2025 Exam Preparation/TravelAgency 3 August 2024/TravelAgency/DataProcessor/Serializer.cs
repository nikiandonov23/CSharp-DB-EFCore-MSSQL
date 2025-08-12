using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using TravelAgency.Data;
using TravelAgency.Data.Models;
using TravelAgency.Data.Models.Enums;
using TravelAgency.DataProcessor.ExportDtos;

namespace TravelAgency.DataProcessor
{
    public class Serializer
    {
        public static string ExportGuidesWithSpanishLanguageWithAllTheirTourPackages(TravelAgencyContext context)
        {
            // Извличаме всички водачи, които говорят испански
            var guides = context.Guides
                .Where(g => g.Language == Language.Spanish)
                .Select(g => new
                {
                    g.FullName,
                    TourPackages = g.TourPackagesGuides
                        .Select(tpg => tpg.TourPackage)
                        .OrderByDescending(tp => tp.Price)  // Първо по цена низходящо
                        .ThenBy(tp => tp.PackageName)  // Ако цената е еднаква, подреждаме по име
                        .ToList()
                })
                .OrderByDescending(g => g.TourPackages.Count)  // Подреждаме водачите по брой пакети
                .ThenBy(g => g.FullName)  // Ако имат еднакъв брой пакети, подреждаме по име
                .ToList();

            // Създаваме DTO обекти за износ
            var exportGuides = guides.Select(g => new ExportGuidesWithPackages
            {
                FullName = g.FullName,
                TourPackages = g.TourPackages.Select(tp => new ExportTourPackages
                {
                    Name = tp.PackageName,
                    Description = tp.Description,
                    Price = tp.Price
                }).ToList()
            }).ToList();

            // Използваме XmlSerializer, за да създадем XML износ
            var xmlSerializer = new XmlSerializer(typeof(List<ExportGuidesWithPackages>), new XmlRootAttribute("Guides"));
            var sb = new StringBuilder();
            using (var stringWriter = new System.IO.StringWriter(sb))
            {
                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");

                xmlSerializer.Serialize(stringWriter, exportGuides,namespaces);
            }

            return sb.ToString();
        }

        


        public static string ExportCustomersThatHaveBookedHorseRidingTourPackage(TravelAgencyContext context)
        {
            var validCustomers = context
                .Customers
                .Where(c => c.Bookings
                    .Any(b => b.TourPackage.PackageName == "Horse Riding Tour")) // Избиране на клиенти, които имат поне една резервация за "Horse Riding Tour"
             
                .OrderBy(c => c.FullName) // Подреждаме по име (азбучно), когато броят на резервациите е същият
                .ThenByDescending(c => c.Bookings.Count) // Подреждаме по брой резервации (низходящо)
                .Select(c => new
                {
                    c.FullName,
                    c.PhoneNumber,
                    Bookings = c.Bookings
                        .Where(b => b.TourPackage.PackageName == "Horse Riding Tour") // Филтриране на резервациите за "Horse Riding Tour"
                        .OrderBy(b => b.BookingDate) // Подреждаме резервациите по дата (възходящо)
                        .Select(b => new
                        {
                            TourPackageName = b.TourPackage.PackageName,
                            Date = b.BookingDate.ToString("yyyy-MM-dd") // Форматираме датата в желания формат
                        })
                        .ToArray()
                })
                .ToArray();



            string result = JsonConvert.SerializeObject(validCustomers, Formatting.Indented);
           return result;

               
        }
    }
}
