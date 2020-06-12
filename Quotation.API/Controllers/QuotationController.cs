using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Quotation.API.Data;
using Quotation.API.Model;
using Quotation.API.Utilities;


namespace Quotation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotationController : ControllerBase
    {
        private readonly ILogger<QuotationController> _logger;
        private readonly IQuotationRepository _quotationRepository;
        private HttpClient _httpClient;
        private readonly string _url = @"http://www.bancoprovincia.com.ar/Principal/Dolar";

        public QuotationController(ILogger<QuotationController> logger, IQuotationRepository quotationRepository)
        {
            _logger = logger;
            _quotationRepository = quotationRepository;
        }
      
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Fetching all the quotes from the storage");

            throw new Exception("Exception while fetching all the quotes from the storage.");
        }

        [HttpGet("currency")]
        public async Task<IActionResult> Get(Currencies? currency)
        {
            try
            {
                _logger.LogInformation("Fetching all the currencies from the storage");

                var quotation = await _quotationRepository.GetQuotation(currency);
                if (quotation == null)
                    return NotFound(new NotFoundError("The quotation was not found"));

                return Ok(quotation);

            }
            catch (Exception e)
            {
                _logger.LogError("error: " + e.Message);
                return BadRequest(new InternalServerError("The quotation was not found"));
            }
        }
    }
}