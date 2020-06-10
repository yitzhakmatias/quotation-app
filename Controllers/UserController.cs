using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quotation.API.DataContext;


namespace Quotation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly QuotationContext _quotationContext;
        
        public UserController(QuotationContext quotationContext)
        {
            _quotationContext = quotationContext;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _quotationContext.Users.ToListAsync() );
        }
    }
}