using System.Text;
using Newtonsoft.Json;
using Trucks.Data.Models;
using Trucks.Data.Models.Enums;
using Trucks.DataProcessor.ImportDto;
using Trucks.Utilities;

namespace Trucks.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using Data;


    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedDespatcher
            = "Successfully imported despatcher - {0} with {1} trucks.";

        private const string SuccessfullyImportedClient
            = "Successfully imported client - {0} with {1} trucks.";

        public static string ImportDespatcher(TrucksContext context, string xmlString)
        {

            const string xmlRoot = "Despatchers";

            StringBuilder output = new StringBuilder();


            ImportDespatcherTrucksDto[]? despatcherDtos = XmlHelper
                .Deserialize<ImportDespatcherTrucksDto[]>(xmlString, xmlRoot);


            ICollection<Despatcher> validDespatchers = new List<Despatcher>();



            if (despatcherDtos != null && despatcherDtos.Length > 0)
            {
                foreach (var despatcherDto in despatcherDtos)
                {
                    if (!IsValid(despatcherDto))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    Despatcher currentDespatcher = new Despatcher()   //nov Despatcher s prazen COLLECTIO
                    {
                        Name = despatcherDto.Name,
                        Position = despatcherDto.Position,
                        Trucks = new List<Truck>()
                    };


                    foreach (var truck in despatcherDto.Trucks)
                    {
                        if (!IsValid(truck))
                        {
                            output.AppendLine(ErrorMessage);
                            continue;
                        }

                        Truck currentTruck = new Truck()
                        {
                            RegistrationNumber = truck.RegistrationNumber,
                            VinNumber = truck.VinNumber,
                            TankCapacity = truck.TankCapacity,
                            CargoCapacity = truck.CargoCapacity,
                            CategoryType = (CategoryType)truck.CategoryType,
                            MakeType = (MakeType)truck.MakeType

                        };

                        currentDespatcher.Trucks.Add(currentTruck);

                    }


                    validDespatchers.Add(currentDespatcher);
                    output.AppendLine(string.Format(SuccessfullyImportedDespatcher, currentDespatcher.Name,
                        currentDespatcher.Trucks.Count));

                }

                context.Despatchers.AddRange(validDespatchers);
                context.SaveChanges();

            }



            return output.ToString().TrimEnd();
        }




        public static string ImportClient(TrucksContext context, string jsonString)
        {
            StringBuilder output = new StringBuilder();


            ImportClientTrucksJsonDto[]? clientDtos =
                JsonConvert.DeserializeObject<ImportClientTrucksJsonDto[]>(jsonString);


            ICollection<Client> validClients = new List<Client>();

            if (clientDtos != null && clientDtos.Length > 0)
            {



                foreach (var clientDto in clientDtos)
                {
                    if (!IsValid(clientDto) || clientDto.Type == "usual")
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    Client currentClient = new Client()    //client s prazen COLLECTION
                    {
                        Name = clientDto.Name,
                        Nationality = clientDto.Nationality,
                        Type = clientDto.Type,
                        ClientsTrucks = new List<ClientTruck>()
                    };




                    foreach (var truckInt in clientDto.Trucks.Distinct())
                    {
                        bool isTruckExistInDb = context.Trucks.Any(x => x.Id == truckInt);
                        if (!isTruckExistInDb)
                        {
                            output.AppendLine(ErrorMessage);
                            continue;
                        }


                        ClientTruck currentTruck = new ClientTruck()
                        {
                            TruckId = truckInt ,                    //tuka moje da iska i drugite propertita da se dobavqt...
                            Client = currentClient  //BEZ DA KAJA RELACIQTA SHTE GURMI !!!!
                        };

                        currentClient.ClientsTrucks.Add(currentTruck);
                    }

                    validClients.Add(currentClient);
                    output.AppendLine(string.Format(SuccessfullyImportedClient, currentClient.Name,
                        currentClient.ClientsTrucks.Count));

                }

                context.Clients.AddRange(validClients);
                context.SaveChanges();

            }


            return output.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}