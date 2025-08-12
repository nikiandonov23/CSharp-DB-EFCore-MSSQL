using System.Globalization;
using System.Text;
using Invoices.Data.Models;
using Invoices.Data.Models.Enums;
using Invoices.DataProcessor.ImportDto;
using Invoices.Utilities;
using Newtonsoft.Json;

namespace Invoices.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using Invoices.Data;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedClients
            = "Successfully imported client {0}.";

        private const string SuccessfullyImportedInvoices
            = "Successfully imported invoice with number {0}.";

        private const string SuccessfullyImportedProducts
            = "Successfully imported product - {0} with {1} clients.";


        public static string ImportClients(InvoicesContext context, string xmlString)
        {

            const string xmlRoot = "Clients";

            StringBuilder output = new StringBuilder();

            ImportClientAdressesDto[]? clientDtos = XmlHelper
                .Deserialize<ImportClientAdressesDto[]>(xmlString, xmlRoot);

            ICollection<Client> validClients = new List<Client>();

            if (clientDtos!=null&&clientDtos.Length>0)
            {

                foreach (var clientDto in clientDtos)
                {

                    if (!IsValid(clientDto))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }


                    Client currentClient = new Client()  //клиент с празен адрес
                    {

                        Name = clientDto.Name,
                        NumberVat = clientDto.NumberVat,
                        Addresses = new List<Address>()
                        
                    };

                    foreach (var address in clientDto.Addresses)
                    {
                        if (!IsValid(address))// taq validaciq moje da ne e dostaty4na
                        {
                            output.AppendLine(ErrorMessage);
                            continue;
                        }

                        Address currentAddress = new Address()
                        {
                            StreetName = address.StreetName,
                            StreetNumber = address.StreetNumber,
                            PostCode = address.PostCode,
                            City = address.City,
                            Country = address.Country

                        };
                        currentClient.Addresses.Add(currentAddress);

                    }

                    validClients.Add(currentClient);
                    output.AppendLine(string.Format(SuccessfullyImportedClients,currentClient.Name));

                }



            }
            context.Clients.AddRange(validClients);
            context.SaveChanges();
            return output.ToString().TrimEnd();
        }


        public static string ImportInvoices(InvoicesContext context, string jsonString)
        {
            ImportInvoicesDto[]? invoicesDtos = JsonConvert
                .DeserializeObject<ImportInvoicesDto[]>(jsonString);

            StringBuilder output=new StringBuilder();

            ICollection<Invoice> validInvoices = new List<Invoice>();

            if (invoicesDtos!=null&&invoicesDtos.Length>0)
            {
                foreach (var invoiceDto in invoicesDtos)
                {
                    if (!IsValid(invoiceDto))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    bool isDueDateValidFormat = DateTime
                        .TryParseExact(invoiceDto.DueDate, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out DateTime dueDate);

                    bool isIssueDateValidFormat = DateTime
                        .TryParseExact(invoiceDto.IssueDate, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out DateTime issueDate);

                    if (!isDueDateValidFormat||!isIssueDateValidFormat||dueDate<issueDate)
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    Invoice currentInvoice = new Invoice()
                    {
                        Number = invoiceDto.Number,
                        IssueDate = issueDate,
                        DueDate = dueDate,
                        Amount = invoiceDto.Amount,
                        CurrencyType = (CurrencyType)invoiceDto.CurrencyType,
                        ClientId = invoiceDto.ClientId

                    };

                    validInvoices.Add(currentInvoice);
                    output.AppendLine(string
                        .Format(SuccessfullyImportedInvoices, currentInvoice.Number));

                }

                context.Invoices.AddRange(validInvoices);
                context.SaveChanges();

            }

            return output.ToString().TrimEnd();
        }

        public static string ImportProducts(InvoicesContext context, string jsonString)
        {
            StringBuilder output = new StringBuilder();

            ImportProductClientsDto[]? productDtos =
                JsonConvert.DeserializeObject<ImportProductClientsDto[]>(jsonString);

            ICollection<Product> validProducts = new List<Product>();
            


            if (productDtos!=null&&productDtos.Length>0)
            {
                foreach (var productDto in productDtos)
                {
                    if (!IsValid(productDto))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    Product currentProduct = new Product() //product s prazni klienti
                    {
                        Name = productDto.Name,
                        Price = productDto.Price,
                        CategoryType = (CategoryType)productDto.CategoryType,
                        ProductsClients = new List<ProductClient>()
                    };


                    
                    foreach (var clientId in productDto.Clients.Distinct())
                    {
                        //	If a client does not exist in the database,

                        bool isClientExistInDB = context.Clients
                            .Any(x => x.Id == clientId);

                        //append an error message to the method output and continue with the next client.
                        if (!isClientExistInDB)
                        {
                            output.AppendLine(ErrorMessage);
                            continue;
                        }

                        ProductClient currentClient = new ProductClient()
                        {
                           ClientId = clientId,
                           
                        };



                        currentProduct.ProductsClients.Add(currentClient);

                    }

                    validProducts.Add(currentProduct);
                    output.AppendLine(string.Format(SuccessfullyImportedProducts,currentProduct.Name,
                        currentProduct.ProductsClients.Count));

                }  

                context.AddRange(validProducts);
                context.SaveChanges();
            }


            return output.ToString().TrimEnd();
        }

        public static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    } 
}
