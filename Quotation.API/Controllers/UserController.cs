using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quotation.API.DataContext;
using Quotation.API.Utilities;


namespace Quotation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly QuotationContext _quotationContext;
        private readonly ILogger<UserController> _logger;
        public UserController(QuotationContext quotationContext, ILogger<UserController> logger)
        {
            _quotationContext = quotationContext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _quotationContext.Users.ToListAsync());
            }
            catch (Exception e)
            {
                _logger.LogError("error: " + e.Message);
                return BadRequest(new InternalServerError("error was found"));
            }

       
        }
    }
}