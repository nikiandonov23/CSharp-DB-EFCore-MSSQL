using NetPay.Data;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using NetPay.Data.Models;
using NetPay.Data.Models.Enums;
using NetPay.DataProcessor.ImportDtos;
using NetPay.Utilities;
using Newtonsoft.Json;

namespace NetPay.DataProcessor
{
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data format!";
        private const string DuplicationDataMessage = "Error! Data duplicated.";
        private const string SuccessfullyImportedHousehold = "Successfully imported household. Contact person: {0}";
        private const string SuccessfullyImportedExpense = "Successfully imported expense. {0}, Amount: {1}";

        public static string ImportHouseholds(NetPayContext context, string xmlString)
        {

            const string xmlRoot = "Households";

            StringBuilder output = new StringBuilder();

            ImportHouseholdsDto[]? houseDtos = XmlHelper
                .Deserialize<ImportHouseholdsDto[]>(xmlString, xmlRoot);



            if (houseDtos!=null&&houseDtos.Length>0)
            {


                foreach (var houseDto in houseDtos)
                {
                    if (!IsValid(houseDto))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    bool isItDupplicated = context.Households    //PROVERKA V DB SAMO !!!
                        .Any(x => x.ContactPerson == houseDto.ContactPerson ||
                                  x.Email == houseDto.Email ||
                                  x.PhoneNumber == houseDto.phone);


                    if (isItDupplicated)
                    {
                        output.AppendLine(DuplicationDataMessage);
                        continue;
                    }


                    Household currentHousehold = new Household()
                    {
                        PhoneNumber = houseDto.phone,
                        ContactPerson = houseDto.ContactPerson,
                        Email = houseDto.Email

                    };


                    context.Households.Add(currentHousehold);
                    context.SaveChanges();
                    output.AppendLine(string.Format(SuccessfullyImportedHousehold, currentHousehold.ContactPerson));

                }



            }



            return output.ToString().TrimEnd();
        }

        public static string ImportExpenses(NetPayContext context, string jsonString)
        {
            StringBuilder output = new StringBuilder();


            ImportExpencesDto[]? expenceDtos = JsonConvert
                .DeserializeObject<ImportExpencesDto[]>(jsonString);



            ICollection<Expense> validExpences = new List<Expense>();

            if (expenceDtos!=null&&expenceDtos.Length>0)
            {

                foreach (var expenceDto in expenceDtos)
                {
                    if (!IsValid(expenceDto))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    bool isHouseholdIdValid = context.Households.Any(x => x.Id == expenceDto.HouseholdId);
                    bool isServiceIdValid = context.Services.Any(x => x.Id == expenceDto.ServiceId);


                    if (!isHouseholdIdValid||!isServiceIdValid)
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    bool isDueDateValid = DateTime
                        .TryParseExact(expenceDto.DueDate, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out DateTime dueDate);

                    bool isPaymentStatusEnumValid = Enum
                        .TryParse<PaymentStatus>(expenceDto.PaymentStatus, out PaymentStatus paymentStatus);

                    if (!isPaymentStatusEnumValid||!isDueDateValid)
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }



                    Expense currentExpence = new Expense()
                    {
                        ExpenseName = expenceDto.ExpenseName,
                        Amount = expenceDto.Amount,
                        DueDate = dueDate,
                        PaymentStatus = paymentStatus,
                        HouseholdId = expenceDto.HouseholdId,
                        ServiceId = expenceDto.ServiceId

                    };

                    validExpences.Add(currentExpence);
                    output.AppendLine(string.Format(SuccessfullyImportedExpense, currentExpence.ExpenseName,
                        currentExpence.Amount.ToString("F2")));
                }


                context.Expenses.AddRange(validExpences);
                context.SaveChanges();

            }

            return output.ToString().TrimEnd();

        }

        public static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            foreach(var result in validationResults)
            {
                string currvValidationMessage = result.ErrorMessage;
            }

            return isValid;
        }
    }
}
