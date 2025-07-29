using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Services;


//BU SINIFA GEREK KALMADI ARTIK ÇÜNKÜ TASK SCHEDULER İLE HALLETTİM DİREKT 
public class CurrencyBackgroundService : BackgroundService
{
    private readonly TcmbService _tcmbService;

    public CurrencyBackgroundService(TcmbService tcmbService)
    {
        _tcmbService = tcmbService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _tcmbService.GetAllCurrenciesAsync();
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
