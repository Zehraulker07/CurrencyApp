using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using WebApi.Models;

namespace WebApi.Services
{
    public class TcmbService
    {
        private readonly HttpClient _httpClient;

        public TcmbService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<Currency> GetCurrencyAsync(string kod)
        {
            string url = "https://www.tcmb.gov.tr/kurlar/today.xml";
            string xmlContent = await _httpClient.GetStringAsync(url);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlContent);
            XmlNode currencyNode = xmlDoc.SelectSingleNode($"//Currency[@Kod='{kod}']");

            if (currencyNode == null)
                return null;
            return new Currency
            {
            Code = kod,
            Name = currencyNode.SelectSingleNode("Isim").InnerText,
            Unit = int.Parse(currencyNode.SelectSingleNode("Unit").InnerText),
            ForexBuying = decimal.Parse(currencyNode.SelectSingleNode("ForexBuying").InnerText.Replace('.', ',')),
            ForexSelling = decimal.Parse(currencyNode.SelectSingleNode("ForexSelling").InnerText.Replace('.', ',')),
            BanknoteBuying = decimal.Parse(currencyNode.SelectSingleNode("BanknoteBuying").InnerText.Replace('.', ',')),
            BanknoteSelling = decimal.Parse(currencyNode.SelectSingleNode("BanknoteSelling").InnerText.Replace('.', ','))
            };
        }

        public async Task<List<Currency>> GetAllCurrenciesAsync()
        {
        string url = "https://www.tcmb.gov.tr/kurlar/today.xml";
        string xmlContent = await _httpClient.GetStringAsync(url);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlContent);
        XmlNodeList currencyNodes = xmlDoc.SelectNodes("//Currency");
        List<Currency> currencies = new List<Currency>();

            foreach (XmlNode node in currencyNodes)
            {
                try
                {
                    var currency = new Currency
                    {
                        Code = node.Attributes["Kod"]?.Value,
                        Name = node.SelectSingleNode("Isim")?.InnerText,
                        Unit = int.Parse(node.SelectSingleNode("Unit")?.InnerText),
                        ForexBuying = TryParseDecimal(node.SelectSingleNode("ForexBuying")?.InnerText.Replace('.', ',')),
                        ForexSelling = TryParseDecimal(node.SelectSingleNode("ForexSelling")?.InnerText.Replace('.', ',')),
                        BanknoteBuying = TryParseDecimal(node.SelectSingleNode("BanknoteBuying")?.InnerText.Replace('.', ',')),
                        BanknoteSelling = TryParseDecimal(node.SelectSingleNode("BanknoteSelling")?.InnerText.Replace('.', ','))
                    };

                    currencies.Add(currency);
                }
                catch
                {
                    // Bazı döviz türlerinde eksik veri olabilir, onları atlıyoruz
                    continue;
                }
    }
      return currencies;
}

            private decimal TryParseDecimal(string value)
        {
            return decimal.TryParse(value?.Replace('.', ','), out var result) ? result : 0;
        }

            private int TryParseInt(string value)
            {
                return int.TryParse(value, out var result) ? result : 1;
            }  
    }
}
