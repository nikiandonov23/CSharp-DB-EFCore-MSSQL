using System.Text;
using Medicines.Data.Models;
using Medicines.Data.Models.Enums;
using Medicines.DataProcessor.ExportDtos;
using Medicines.DataProcessor.ImportDtos;
using Medicines.Utilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Medicines.DataProcessor
{
    using Medicines.Data;
    using System.Diagnostics;
    using System.Xml.Linq;

    public class Serializer
    {
        public static string ExportPatientsWithTheirMedicines(MedicinesContext context, string date)
        {

            const string xmlRoot = "Patients";

            var patients = context.Patients
                
                .AsEnumerable()
                .Select(x => new ExportPatientMedicinesDto()
                {
                    Gender = x.Gender.ToString().ToLower(),
                    Name = x.FullName,
                    AgeGroup = x.AgeGroup.ToString(),
                    Medicines = x.PatientsMedicines
                        .Where(x=>x.Medicine.ProductionDate>DateTime.Parse(date))
                        .AsEnumerable()
                        .Select(x=>new ExportMedicineDto()
                        {
                            Category=x.Medicine.Category.ToString().ToLower(),
                            Name=x.Medicine.Name,
                            Price=x.Medicine.Price.ToString("F2"),
                            Producer=x.Medicine.Producer,
                            BestBefore=x.Medicine.ExpiryDate.ToString("yyyy-MM-dd")

                        }).OrderByDescending(x=>x.BestBefore)
                        .ThenBy(x=>decimal.Parse(x.Price))  //!!! podrejdaneto e stringovo !!!!
                        .ToArray()


                })
                .Where(x=>x.Medicines.Length>0)
                .OrderByDescending(x=>x.Medicines.Length)
                .ThenBy(x=>x.Name)
                .ToArray();

            string result = XmlHelper.Serialize(patients, xmlRoot);
            return result;
        }

        public static string ExportMedicinesFromDesiredCategoryInNonStopPharmacies(MedicinesContext context, int medicineCategory)
        {
            var medicines = context.Medicines.AsNoTracking()
                .Where(x => x.Pharmacy.IsNonStop == true&&x.Category==(Category)medicineCategory)
                .OrderBy(x => x.Price)
                .ThenBy(x => x.Name)
                .Select(x => new

                {
                    Name = x.Name,
                    Price = x.Price.ToString("F2"),
                    Pharmacy = new
                    {
                        Name = x.Pharmacy.Name,
                        PhoneNumber = x.Pharmacy.PhoneNumber
                    }
                }).ToArray();


            string result = JsonConvert.SerializeObject(medicines, Formatting.Indented);
            return result;
        }
    }
}
