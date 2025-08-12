using System.Xml.Serialization;
using Invoices.Data.Models;

namespace Invoices.DataProcessor.ExportDto;


[XmlType(nameof(Client))]
public class ExportClientInvoicesDto
{

    [XmlAttribute(nameof(InvoicesCount))]
    public int InvoicesCount { get; set; }


    [XmlElement(nameof(ClientName))]
    public string ClientName { get; set; } = null!;


    [XmlElement(nameof(VatNumber))]
    public string VatNumber { get; set; } = null!;


    [XmlArray(nameof(Invoices))]
    [XmlArrayItem("Invoice")]
    public ExportInvoicesDto[] Invoices { get; set; } = null!;
}