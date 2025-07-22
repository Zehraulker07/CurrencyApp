using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using WebApi.Models;

namespace WebApi.Services
{
    public class TcmbService
    {
        private readonly HttpClient _httpClient;
        //private readonly string _baseUrl;

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
                ForexBuying = decimal.Parse(currencyNode.SelectSingleNode("ForexBuying").InnerText.Replace('.', ',')),
                ForexSelling = decimal.Parse(currencyNode.SelectSingleNode("ForexSelling").InnerText.Replace('.', ','))
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
                ForexBuying = decimal.Parse(node.SelectSingleNode("ForexBuying")?.InnerText.Replace('.', ',')),
                ForexSelling = decimal.Parse(node.SelectSingleNode("ForexSelling")?.InnerText.Replace('.', ','))
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


        
    }
}
