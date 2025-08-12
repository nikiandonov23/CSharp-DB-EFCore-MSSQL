using Trucks.Data.Models.Enums;
using Trucks.DataProcessor.ExportDto;

namespace Trucks.DataProcessor
{
    using Data;
    using Newtonsoft.Json;
    using Trucks.Utilities;

    public class Serializer
    {
        public static string ExportDespatchersWithTheirTrucks(TrucksContext context)
        {
            const string xmlRoot = "Despatchers";

            ExportDespatcherTrucksXmlDto[] validDespatchers = context.Despatchers
                .Where(x => x.Trucks.Count > 0)
                .Select(x => new ExportDespatcherTrucksXmlDto()
                {
                    TrucksCount = x.Trucks.Count,
                    DespatcherName = x.Name,
                    Trucks = x.Trucks.Select(x => new ExportTrucksXmlDto()
                    {
                        RegistrationNumber = x.RegistrationNumber,
                        Make = x.MakeType.ToString(),


                    })
                        .OrderBy(x => x.RegistrationNumber)
                        .ToArray()



                }).OrderByDescending(x=>x.Trucks.Length)
                .ThenBy(x=>x.DespatcherName)
                .ToArray();

            string result = XmlHelper.Serialize(validDespatchers, xmlRoot);
            return result;
        }

        public static string ExportClientsWithMostTrucks(TrucksContext context, int capacity)
        {
            var validClients = context.Clients
                .Where(x => x.ClientsTrucks.Any(x => x.Truck.TankCapacity >= capacity))
                .AsEnumerable()
                .Select(x => new ExportClientTrucksJsonDto()
                {
                    Name = x.Name,
                    Trucks = x.ClientsTrucks
                        .Where(x => x.Truck.TankCapacity >= capacity)
                        .Select(x => new ExportTrucksJsonDto()
                        {
                            TruckRegistrationNumber = x.Truck.RegistrationNumber,
                            VinNumber = x.Truck.VinNumber,
                            TankCapacity = x.Truck.TankCapacity,
                            CargoCapacity = x.Truck.CargoCapacity,
                            CategoryType = x.Truck.CategoryType.ToString(),
                            MakeType = x.Truck.MakeType.ToString(),

                        }).OrderBy(x => x.MakeType)
                        .ThenByDescending(x => x.CargoCapacity)
                        .ToArray()

                })
                .OrderByDescending(x => x.Trucks.Length)
                .ThenBy(x => x.Name)
                .ToArray()
                .Take(10);

            string result = JsonConvert.SerializeObject(validClients, Formatting.Indented);
            return result;
        }
    }
}
