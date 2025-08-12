using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Text;
using System.Globalization;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Schema;
using Newtonsoft.Json;
using TravelAgency.Data;
using TravelAgency.Data.Models;

using TravelAgency.DataProcessor.ImportDtos;
using TravelAgency.Utilities;

namespace TravelAgency.DataProcessor
{
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data format!";
        private const string DuplicationDataMessage = "Error! Data duplicated.";
        private const string SuccessfullyImportedCustomer = "Successfully imported customer - {0}";
        private const string SuccessfullyImportedBooking = "Successfully imported booking. TourPackage: {0}, Date: {1}";

        public static string ImportCustomers(TravelAgencyContext context, string xmlString)
        {

            const string xmlRoot = "Customers";

            StringBuilder output = new StringBuilder();

            ImportCustomersDto[]? customerDtos = XmlHelper
                .Deserialize<ImportCustomersDto[]>(xmlString, xmlRoot);


            ICollection<Customer> validCustomers = new List<Customer>();

            if (customerDtos.Length > 0 && customerDtos != null)
            {

                foreach (var customerDto in customerDtos)
                {
                    if (!IsValid(customerDto))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    bool isDuplicatedInContext = context.Customers
                        .Any
                        (
                            x => x.FullName == customerDto.FullName ||
                                 x.Email == customerDto.Email ||
                                 x.PhoneNumber == customerDto.PhoneNumber
                        );

                    bool isDuplicatedInValidCustomers = validCustomers
                        .Any
                        (
                            x => x.FullName == customerDto.FullName ||
                            x.Email == customerDto.Email ||
                            x.PhoneNumber == customerDto.PhoneNumber
                        );

                    if (isDuplicatedInContext || isDuplicatedInValidCustomers)
                    {
                        output.AppendLine(DuplicationDataMessage);
                        continue;

                    }

                    Customer currentCustromer = new Customer()
                    {
                        FullName = customerDto.FullName,
                        Email = customerDto.Email,
                        PhoneNumber = customerDto.PhoneNumber,
                    };

                    validCustomers.Add(currentCustromer);
                    output.AppendLine(string.Format(SuccessfullyImportedCustomer, currentCustromer.FullName));

                }

                context.AddRange(validCustomers);
                context.SaveChanges();

            }


            return output.ToString().TrimEnd();
        }

        public static string ImportBookings(TravelAgencyContext context, string jsonString)
        {

            StringBuilder output = new StringBuilder();

            ImportBookingDto[]? bookingDtos = JsonConvert
                .DeserializeObject<ImportBookingDto[]>(jsonString);

            ICollection<Booking> validBookings = new List<Booking>();

            if (bookingDtos.Length > 0 && bookingDtos != null)
            {
                foreach (var bookingDto in bookingDtos)
                {
                    if (!IsValid(bookingDto))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }


                    bool isDateValid = DateTime
                        .TryParseExact(bookingDto.BookingDate, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out DateTime dueDate);  //outva mi duedate !!





                    if (!isDateValid)
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    bool isDuplicatedNameInContext = context
                        .Customers.Any(x => x.FullName == bookingDto.CustomerName);

                    bool isDuplicatedTourPackagerNameInContext = context
                        .TourPackages.Any(x => x.PackageName == bookingDto.TourPackageName);

                    if (!isDuplicatedNameInContext || !isDuplicatedTourPackagerNameInContext)
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    Booking currentBooking = new Booking()
                    {
                        BookingDate = dueDate,
                        Customer = context.Customers.FirstOrDefault(x => x.FullName == bookingDto.CustomerName),
                        TourPackage = context.TourPackages.FirstOrDefault(x => x.PackageName == bookingDto.TourPackageName)
                    };
                    if (currentBooking.Customer != null && currentBooking.TourPackage != null)
                    {
                        validBookings.Add(currentBooking);
                        output.AppendLine(string
                            .Format(SuccessfullyImportedBooking, currentBooking.TourPackage.PackageName,
                                currentBooking.BookingDate.ToString("yyyy-MM-dd")));
                    }

                }

                context.AddRange(validBookings);
                context.SaveChanges();

            }



            return output.ToString().TrimEnd();
        }

        public static bool IsValid(object dto)
        {
            var validateContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(dto, validateContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                string currValidationMessage = validationResult.ErrorMessage;
            }

            return isValid;
        }
    }
}
