using System.Security.AccessControl;
using System.Xml.Serialization;
using Invoices.Data.Models;

namespace Invoices.DataProcessor.ExportDto;


[XmlType(nameof(Invoice))]
public class ExportInvoicesDto
{

    [XmlElement(nameof(InvoiceNumber))]
    public string InvoiceNumber { get; set; } = null!;



    [XmlElement(nameof(InvoiceAmount))]
    public string InvoiceAmount { get; set; } = null!;



    [XmlElement(nameof(DueDate))]
    public string DueDate { get; set; } = null!;



    [XmlElement(nameof(Currency))]
    public string Currency { get; set; } = null!;
}