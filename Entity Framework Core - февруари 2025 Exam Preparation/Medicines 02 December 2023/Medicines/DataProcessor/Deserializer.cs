using System.Globalization;
using Medicines.Data.Models.Enums;
using Newtonsoft.Json;

namespace Medicines.DataProcessor
{
    using Medicines.Data;
    using Medicines.Data.Models;
    using Medicines.DataProcessor.ImportDtos;
    using Medicines.Utilities;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data!";
        private const string SuccessfullyImportedPharmacy = "Successfully imported pharmacy - {0} with {1} medicines.";
        private const string SuccessfullyImportedPatient = "Successfully imported patient - {0} with {1} medicines.";

        public static string ImportPatients(MedicinesContext context, string jsonString)
        {
            StringBuilder output = new StringBuilder();

            ImportPatientMedicinesDto[]? patientDtos = JsonConvert
                .DeserializeObject<ImportPatientMedicinesDto[]>(jsonString);

            ICollection<Patient> validPatients = new List<Patient>();


            if (patientDtos!=null&&patientDtos.Length>0)
            {
                foreach (var patientDto in patientDtos)
                {
                    if (!IsValid(patientDto))  //proverqvam dali atrubitite na Dto-to MAIKA sa validni
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }



                    Patient currentPatient = new Patient()  //Patient s prazen collection
                    {
                        FullName = patientDto.FullName,
                        AgeGroup = (AgeGroup)patientDto.AgeGroup,
                        Gender = (Gender)patientDto.Gender,
                        PatientsMedicines = new List<PatientMedicine>()

                    };

                    //proverqvam dali DTO-to maika v kolekciqta si int[] Medicines sudurja ID-to na CurrentPatient-a
                    foreach (int medicineId in patientDto.Medicines)  
                    {

                        //ako currentPatient-a sudurja INT-a medicineId
                        if (currentPatient.PatientsMedicines.Any(x=>x.MedicineId==medicineId))
                        {
                            output.AppendLine(ErrorMessage);
                            continue;

                        }


                        //suzdavam entity MAPPING type 
                        PatientMedicine currentMedicine = new PatientMedicine() 
                        {
                            // DAVAM MU INT STOINOSTTA
                            MedicineId = medicineId,
                        };
                        currentPatient.PatientsMedicines.Add(currentMedicine);
                    }

                    validPatients.Add(currentPatient);
                    output.AppendLine(string
                        .Format(SuccessfullyImportedPatient, currentPatient.FullName,
                            currentPatient.PatientsMedicines.Count));

                }

                context.AddRange(validPatients);
                context.SaveChanges();

            }






            return output.ToString().TrimEnd();
        }

        public static string ImportPharmacies(MedicinesContext context, string xmlString)
        {
            const string xmlRoot = "Pharmacies";

            StringBuilder output = new StringBuilder();


            ImportPharmacyMedicinesDto[]? pharmacyDtos = XmlHelper
                .Deserialize<ImportPharmacyMedicinesDto[]>(xmlString, xmlRoot);

            ICollection<Pharmacy> validPharmacies = new List<Pharmacy>();

            if (pharmacyDtos != null && pharmacyDtos.Length > 0)
            {
                foreach (var pharmacyDto in pharmacyDtos)
                {

                    if (!IsValid(pharmacyDto))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }




                    var currentPharmacy = new Pharmacy()  //suzdavam nov obekt
                    {
                        IsNonStop = bool.Parse(pharmacyDto.NonStop),
                        Name = pharmacyDto.Name,
                        PhoneNumber = pharmacyDto.PhoneNumber,
                        Medicines = new List<Medicine>()
                    };





                    foreach (var medicine in pharmacyDto.Medicines) //proverqvam lekarstvata na pharmacy-to
                    {
                        if (!IsValid(medicine))  // invalid price or missing producer
                        {
                            output.AppendLine(ErrorMessage);
                            continue;
                        }

                        bool isProductionDateValidFormat = DateTime
                            .TryParseExact(medicine.ProductionDate, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                                DateTimeStyles.None, out DateTime productionDate);


                        bool isExpiryDateValidFormat = DateTime
                            .TryParseExact(medicine.ExpiryDate, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                                DateTimeStyles.None, out DateTime expiryDate);

                        if (productionDate >= expiryDate || !isProductionDateValidFormat || !isExpiryDateValidFormat)
                        {
                            output.AppendLine(ErrorMessage);
                            continue;
                        }



                        //	If the medicines collection of the current pharmacy contains another medicine
                        //with the same name and same producer,
                        //the record should NOT be added and an error message should be appended to the method output. 
                        bool isMedicineDuplicated = currentPharmacy
                            .Medicines.Any(x => x.Name == medicine.Name &&
                                                x.Producer == medicine.Producer
                            );
                        if (isMedicineDuplicated)
                        {
                            output.AppendLine(ErrorMessage);
                            continue;
                        }
                        // 	However, if the producer is different,
                        // or the medicine is available in another pharmacy with the same name and producer,
                        // the record will be added.

                        var currentMedicine = new Medicine()
                        {
                            Category = (Category)medicine.Category,
                            Name = medicine.Name,
                            Price = medicine.Price,
                            ProductionDate = productionDate,
                            ExpiryDate = expiryDate,
                            Producer = medicine.Producer
                        };


                        currentPharmacy.Medicines.Add(currentMedicine);
                    }

                    validPharmacies.Add(currentPharmacy);
                    output.AppendLine(string
                        .Format(SuccessfullyImportedPharmacy, currentPharmacy.Name, currentPharmacy.Medicines.Count));
                }


                context.Pharmacies.AddRange(validPharmacies);
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
