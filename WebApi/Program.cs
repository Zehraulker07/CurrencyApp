using System;
using System.Threading.Tasks;
using WebApi.Services;
using WebApi.Models;

namespace WebApi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // var tcmbService = new TcmbService();

            // Currency usd = await tcmbService.GetCurrencyAsync("USD");
            // Currency eur = await tcmbService.GetCurrencyAsync("EUR");

            // if (usd != null)
            // {
            //     Console.WriteLine($"USD Alış: {usd.ForexBuying}, Satış: {usd.ForexSelling}");
            // }

            // if (eur != null)
            // {
            //     Console.WriteLine($"EUR Alış: {eur.ForexBuying}, Satış: {eur.ForexSelling}");
            // }

            var tcmbService = new TcmbService();
            var currencies = await tcmbService.GetAllCurrenciesAsync();
            Console.WriteLine($"Currency list length: {currencies.Count}");



            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();

            var app = builder.Build();
            app.MapControllers(); // controllerları aktive eder

            app.Run();

        }
        
    }
}
