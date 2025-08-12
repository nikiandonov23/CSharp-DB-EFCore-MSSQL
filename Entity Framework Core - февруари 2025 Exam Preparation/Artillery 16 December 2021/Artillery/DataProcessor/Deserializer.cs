using System.Text;
using Artillery.Data.Models;
using Artillery.Data.Models.Enums;
using Artillery.DataProcessor.ImportDto;
using Artillery.Utilities;
using Newtonsoft.Json;

namespace Artillery.DataProcessor
{
    using Artillery.Data;
    using System.ComponentModel.DataAnnotations;

    public class Deserializer
    {
        private const string ErrorMessage =
            "Invalid data.";
        private const string SuccessfulImportCountry =
            "Successfully import {0} with {1} army personnel.";
        private const string SuccessfulImportManufacturer =
            "Successfully import manufacturer {0} founded in {1}.";
        private const string SuccessfulImportShell =
            "Successfully import shell caliber #{0} weight {1} kg.";
        private const string SuccessfulImportGun =
            "Successfully import gun {0} with a total weight of {1} kg. and barrel length of {2} m.";

        public static string ImportCountries(ArtilleryContext context, string xmlString)
        {
            StringBuilder output = new StringBuilder();


            const string xmlRoot = "Countries";



            ImportCountryDto[]? countryDtos = XmlHelper
                .Deserialize<ImportCountryDto[]>(xmlString, xmlRoot);
            if (countryDtos != null && countryDtos.Length > 0)
            {
                foreach (var countryDto in countryDtos)
                {
                    if (!IsValid(countryDto))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    Country currentCountry = new Country()
                    {
                        CountryName = countryDto.CountryName,
                        ArmySize = countryDto.ArmySize

                    };
                    context.Countries.Add(currentCountry);
                    context.SaveChanges();
                    output.AppendLine(string.Format(SuccessfulImportCountry, currentCountry.CountryName,
                        currentCountry.ArmySize));

                }


            }
            return output.ToString().TrimEnd();
        }

        public static string ImportManufacturers(ArtilleryContext context, string xmlString)
        {
            StringBuilder output = new StringBuilder();


            const string xmlRoot = "Manufacturers";



            ImportManufacturersDto[]? manufacturerDtos = XmlHelper
                .Deserialize<ImportManufacturersDto[]>(xmlString, xmlRoot);

            if (manufacturerDtos != null && manufacturerDtos.Length > 0)
            {
                foreach (var manufacturerDto in manufacturerDtos)
                {
                    if (!IsValid(manufacturerDto))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    Manufacturer currentManufacturer = new Manufacturer()
                    {
                        ManufacturerName = manufacturerDto.ManufacturerName,
                        Founded = manufacturerDto.Founded
                    };



                    bool isManufacturerAlreadyImported = context.Manufacturers
                        .Any(x => x.ManufacturerName == currentManufacturer.ManufacturerName);

                    if (isManufacturerAlreadyImported)
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    string[] locationParts = currentManufacturer.Founded.Split(", ", StringSplitOptions.RemoveEmptyEntries);
                    string location = string.Join(", ", locationParts.TakeLast(2));
                    output.AppendLine(string.Format(SuccessfulImportManufacturer, currentManufacturer.ManufacturerName, location));
                }


            }

            return output.ToString().TrimEnd();
        }

        public static string ImportShells(ArtilleryContext context, string xmlString)
        {
            StringBuilder output = new StringBuilder();


            const string xmlRoot = "Shells";



            ImportShellsDto[]? shellDtos = XmlHelper
                .Deserialize<ImportShellsDto[]>(xmlString, xmlRoot);

            if (shellDtos != null && shellDtos.Length > 0)
            {
                foreach (var shellDto in shellDtos)
                {
                    if (!IsValid(shellDto))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    Shell currentShell = new Shell()
                    {
                        ShellWeight = shellDto.ShellWeight,
                        Caliber = shellDto.Caliber
                    };


                    context.Shells.Add(currentShell);
                    context.SaveChanges();
                    output.AppendLine(string.Format(SuccessfulImportShell, currentShell.Caliber,
                        currentShell.ShellWeight));
                }
            }




            return output.ToString().TrimEnd();
        }

        public static string ImportGuns(ArtilleryContext context, string jsonString)
        {
            StringBuilder output = new StringBuilder();


            ImportGunCountriesJsonDto[]? gunDtos =
                JsonConvert.DeserializeObject<ImportGunCountriesJsonDto[]>(jsonString);


            if (gunDtos != null && gunDtos.Length > 0)
            {
                foreach (var gunDto in gunDtos)
                {


                    bool isEnumValid = Enum
                        .TryParse<GunType>(gunDto.GunType, out GunType gunType);

                    if (!IsValid(gunDto) || !isEnumValid)
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }




                    Gun currentGun = new Gun()
                    {
                        ManufacturerId = gunDto.ManufacturerId,
                        GunWeight = gunDto.GunWeight,
                        BarrelLength = gunDto.BarrelLength,
                        NumberBuild = gunDto.NumberBuild,
                        Range = gunDto.Range,
                        GunType = gunType,
                        ShellId = gunDto.ShellId,
                        CountriesGuns = gunDto.Countries
                            .Select(x => new CountryGun()
                            {
                                CountryId = x.Id

                            }).ToArray()

                    };

                    context.Guns.Add(currentGun);
                    context.SaveChanges();
                    output.AppendLine(string.Format(SuccessfulImportGun, gunType, currentGun.GunWeight,
                        currentGun.BarrelLength.ToString("F2")));

                }


            }

            return output.ToString().TrimEnd();
        }
        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
    }
}