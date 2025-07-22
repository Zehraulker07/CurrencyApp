using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly TcmbService _tcmbService = new TcmbService();

        [HttpGet("{kod}")]
        public async Task<ActionResult<Currency>> Get(string kod)
        {
            var currency = await _tcmbService.GetCurrencyAsync(kod.ToUpper());

            if (currency == null)
                return NotFound("Kur bulunamadı");

            return Ok(currency);
        }
        

        
        [HttpGet]
        public async Task<ActionResult<List<Currency>>> GetAll()
        {
        var result = await _tcmbService.GetAllCurrenciesAsync();
        return Ok(result);
        }

    }
}
