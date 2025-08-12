using Cadastre.Data;
using Cadastre.Data.Enumerations;
using Cadastre.DataProcessor.ExportDtos;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using Cadastre.Utilities;

namespace Cadastre.DataProcessor
{
    public class Serializer
    {
        public static string ExportPropertiesWithOwners(CadastreContext dbContext)
        {
            var properties = dbContext.Properties
                .Where(x => x.DateOfAcquisition >= new DateTime(2000, 1, 1))
                .OrderByDescending(x => x.DateOfAcquisition)
                .ThenBy(x => x.PropertyIdentifier)
                .Select(x => new
                {
                    PropertyIdentifier = x.PropertyIdentifier,
                    Area = x.Area,
                    Address = x.Address,
                    DateOfAcquisition = x.DateOfAcquisition.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Owners = x.PropertiesCitizens.Select(o => new
                    {
                        LastName = o.Citizen.LastName,
                        MaritalStatus = o.Citizen.MaritalStatus.ToString(), //tuka moje da grumne zaradi ЕНУМ

                    }).OrderBy(x => x.LastName)
                    .ToList()

                }).ToList();

            string result = JsonConvert.SerializeObject(properties, Formatting.Indented);
            return result;

        }

        public static string ExportFilteredPropertiesWithDistrict(CadastreContext dbContext)
        {
            const string xmlRoot = "Properties";

            var properties = dbContext.Properties
                .Where(x => x.Area >= 100)
                .OrderByDescending(x => x.Area)
                .ThenBy(x => x.DateOfAcquisition)
                .Select(x => new ExportPropertyDistrictDto()
                {
                    PostalCode = x.District.PostalCode,
                    PropertyIdentifier = x.PropertyIdentifier,
                    Area = x.Area,
                    DateOfAcquisition = x.DateOfAcquisition.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)

                })
                .ToList();



            return XmlHelper.Serialize(properties,xmlRoot).ToString().TrimEnd();
        }
    }
}
