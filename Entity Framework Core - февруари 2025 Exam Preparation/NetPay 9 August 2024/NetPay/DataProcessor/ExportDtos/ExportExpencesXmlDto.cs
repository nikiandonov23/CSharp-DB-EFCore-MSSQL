using System.Xml.Serialization;
using NetPay.Data.Models;

namespace NetPay.DataProcessor.ExportDtos;



[XmlType(nameof(Expense))]
public class ExportExpencesXmlDto
{

    [XmlElement(nameof(ExpenseName))]
    public string ExpenseName { get; set; } = null!;




    [XmlElement(nameof(Amount))]
    public string Amount { get; set; } = null!;    //VNIMAVAI PRI ORDERA





    [XmlElement(nameof(PaymentDate))]
    public string PaymentDate { get; set; } = null!;   //VNIMAVAI PRI ORDERA





    [XmlElement(nameof(ServiceName))]
    public string ServiceName { get; set; } = null!;
}