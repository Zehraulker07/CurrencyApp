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
            var tcmbService = new TcmbService();
            var currencies = await tcmbService.GetAllCurrenciesAsync();
            //var currency = await tcmbService.GetCurrencyAsync("usd");
            Console.WriteLine($"Currency list length: {currencies.Count}");
            //Console.WriteLine($"USD:{currency}");

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddSingleton<TcmbService>();
            builder.Services.AddHostedService<CurrencyBackgroundService>();
            
            var app = builder.Build();
            app.MapControllers(); // controllerları aktive eder

            app.Run();

        }
        
    }
}
