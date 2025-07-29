using System.Xml.Serialization;

namespace WebApi.Models
{
    [XmlRoot("Tarih_Date")]
public class TarihDate
{
    [XmlElement("Currency")]
    public List<Currency>? CurrencyList { get; set; }
}
    public class Currency
    {
        public string? Code { get; set; }              // Döviz Kodu (USD, EUR vs.)
        public string? Name { get; set; }              // Döviz Cinsi (ABD DOLARI vs.)
        public int Unit { get; set; }                 // Birim (1, 10 vs.)

        public decimal ForexBuying { get; set; }      // Döviz Alış
        public decimal ForexSelling { get; set; }     // Döviz Satış

        public decimal BanknoteBuying { get; set; }   // Efektif Alış
        public decimal BanknoteSelling { get; set; }  // Efektif Satış
    }
}
