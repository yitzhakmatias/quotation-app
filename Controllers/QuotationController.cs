using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Quotation.API.Model;
using Quotation.API.Utilities;


namespace Quotation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotationController : ControllerBase
    {
        private readonly ILogger<QuotationController> _logger;
        private HttpClient _httpClient;
        private readonly string _url = @"http://www.bancoprovincia.com.ar/Principal/Dolar";

        public QuotationController(ILogger<QuotationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        { 
            return NotFound(new NotFoundError("The quotation was not found"));
        }

        [HttpGet("{currency}")]
        public async Task<IActionResult> Get(Currency? currency)
        {
            try
            {
                using (_httpClient = new HttpClient())
                {
                    var content = await _httpClient.GetStringAsync(_url);
                    var response = JArray.Parse(content);

                    switch (currency)
                    {
                        case Currency.Dollar:
                        {
                            return Ok(new Model.Quotation()
                            {
                                Moneda = Currency.Dollar.ToString(),
                                Compra = Convert.ToDouble(response[0]),
                                Venta = Convert.ToDouble(response[1]),
                                Fecha = response[2].ToString()
                            });
                        }
                        case Currency.Real:
                        {
                            return Ok(new Model.Quotation()
                            {
                                Moneda = Currency.Real.ToString(),
                                Compra = Convert.ToDouble(response[0]) / 4,
                                Venta = Convert.ToDouble(response[1]) / 4,
                                Fecha = response[2].ToString()
                            });
                        }
                        default:
                        {
                            return NotFound(new NotFoundError("The quotation was not found"));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(new InternalServerError("The quotation was not found"));
            }
        }
    }
}