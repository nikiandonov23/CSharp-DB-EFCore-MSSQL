using System.Globalization;
using System.Text;
using Cadastre.Data.Enumerations;
using Cadastre.Data.Models;
using Cadastre.DataProcessor.ImportDtos;
using Cadastre.Utilities;
using Newtonsoft.Json;

namespace Cadastre.DataProcessor
{
    using Cadastre.Data;
    using System.ComponentModel.DataAnnotations;

    public class Deserializer
    {
        private const string ErrorMessage =
            "Invalid Data!";
        private const string SuccessfullyImportedDistrict =
            "Successfully imported district - {0} with {1} properties.";
        private const string SuccessfullyImportedCitizen =
            "Succefully imported citizen - {0} {1} with {2} properties.";

        public static string ImportDistricts(CadastreContext dbContext, string xmlDocument)
        {


            StringBuilder output = new StringBuilder();
            const string roonName = "Districts";

            ImportDistrictPropertiesDto[]? districtDtos =
                XmlHelper.Deserialize<ImportDistrictPropertiesDto[]>(xmlDocument, roonName);

            ICollection<District> validDistricts = new List<District>();  //САМО ВАЛИДНИ ДИСТРИКТИ

            if (districtDtos != null && districtDtos.Length > 0)
            {



                foreach (var districtDto in districtDtos)
                {
                    if (!IsValid(districtDto))     // валидирам чрез атрибутите в дто класа
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    //проверявам за дубликата в context-a и във validDistricts

                    bool isDistrictNameDuplicated = dbContext
                                                        .Districts
                                                        .Any(x => x.Name == districtDto.Name) ||
                                                    validDistricts.Any(x => x.Name == districtDto.Name);

                    if (isDistrictNameDuplicated)
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }




                    ICollection<Property> validProperties = new List<Property>();
                    //бъркаме в District и проверяваме Property-тата
                    foreach (var propertyDto in districtDto.Properties)
                    {
                        if (!IsValid(propertyDto))  //валидирам пропъртитата според атрибутите в дто-то
                        {
                            output.AppendLine(ErrorMessage);
                            continue;
                        }


                        bool isDueDateValid = DateTime  //проверявам датата дали е валидна
                            .TryParseExact(propertyDto.DateOfAcquisition, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                                DateTimeStyles.None, out DateTime dateOut);
                        if (!isDueDateValid)
                        {
                            output.AppendLine(ErrorMessage);
                            continue;
                        }

                        //тука може да се наложи да проверя и validDistricts (освен context-a)

                        bool isTherePropertyWithSameId = dbContext.Districts
                            .Any(d => d.Properties
                                .Any(p => p.PropertyIdentifier == propertyDto.PropertyIdentifier));
                        if (isTherePropertyWithSameId)
                        {
                            output.AppendLine(ErrorMessage);
                            continue;
                        }



                        bool isTherePropertyWithSameAddress = dbContext.Districts
                            .Any(d => d.Properties
                                .Any(p => p.Address == propertyDto.Address));
                        if (isTherePropertyWithSameAddress)
                        {
                            output.AppendLine(ErrorMessage);
                            continue;
                        }


                        Property currentProperty = new Property()  //създава пропърти което да вкарам в колекципята
                        {
                            PropertyIdentifier = propertyDto.PropertyIdentifier,
                            Area = propertyDto.Area,
                            Details = propertyDto.Details,
                            Address = propertyDto.Address,
                            DateOfAcquisition = dateOut
                        };

                        validProperties.Add(currentProperty);

                    }

                    District currentDistrict = new District()
                    {
                        Name = districtDto.Name,
                        PostalCode = districtDto.PostalCode,
                        Properties = validProperties

                    };
                    validDistricts.Add(currentDistrict);
                    output.AppendLine(string.Format(SuccessfullyImportedDistrict, currentDistrict.Name,
                        currentDistrict.Properties.Count));

                }

                dbContext.AddRange(validDistricts);
                dbContext.SaveChanges();

            }





            return output.ToString().TrimEnd();

        }

        public static string ImportCitizens(CadastreContext dbContext, string jsonDocument)
        {

            StringBuilder output = new StringBuilder();

            ImportCitizenDto[]? citizenDtos = JsonConvert
                .DeserializeObject<ImportCitizenDto[]>(jsonDocument);


            ICollection<Citizen> validCitizenDtos = new List<Citizen>();

            if (citizenDtos != null && citizenDtos.Length > 0)
            {

                foreach (var citizenDto in citizenDtos)
                {

                    if (!IsValid(citizenDto))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }


                    //maritial status validatiopn
                    bool isPaymentStatusEnumValid = Enum
                        .TryParse<MaritalStatus>(citizenDto.MaritalStatus, out MaritalStatus maritalStatus);
                    if (!isPaymentStatusEnumValid)
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }


                    //validiram datata
                    bool isBirthDateValid = DateTime
                        .TryParseExact(citizenDto.BirthDate, "dd-MM-yyyy", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out DateTime outDate);




                    if (!isBirthDateValid)
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    //в currentCitizen пропъртизитизен е още празно
                    Citizen currentCitizen = new Citizen()
                    {
                        FirstName = citizenDto.FirstName,
                        LastName = citizenDto.LastName,
                        BirthDate = outDate, //convertnato v bool-a
                        MaritalStatus = maritalStatus,//convertnato v bool-a
                        PropertiesCitizens = new HashSet<PropertyCitizen>()


                    };

                    foreach (var propertyId in citizenDto.Properties)
                    {

                        PropertyCitizen currentPropertyCitizen = new PropertyCitizen()
                        {
                          Citizen = currentCitizen,
                          PropertyId = propertyId
                        };

                        currentCitizen.PropertiesCitizens.Add(currentPropertyCitizen);

                    }  


                    validCitizenDtos.Add(currentCitizen);
                    output.AppendLine(string
                        .Format(SuccessfullyImportedCitizen,
                            currentCitizen.FirstName,
                            currentCitizen.LastName,
                            currentCitizen.PropertiesCitizens.Count));

                }

                dbContext.AddRange(validCitizenDtos);
                dbContext.SaveChanges();


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
