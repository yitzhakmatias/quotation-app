using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quotation.API.Data;
using Quotation.API.DTOs;
using Quotation.API.Model;
using Quotation.API.Utilities;


namespace Quotation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IUserInterface _userInterface;

        public PurchaseController(IPurchaseRepository purchaseRepository, IUserInterface userInterface)
        {
            _purchaseRepository = purchaseRepository;
            _userInterface = userInterface;
        }

        private readonly Func<int, Currencies> _getCurrency = (currency) =>
        {
            switch (currency)
            {
                case 1:
                    return Currencies.Dollar;
                case 2:
                    return Currencies.Real;
                default:
                    return 0;
            }
        };

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var res = await _purchaseRepository.GetPurchaseList();
                var purchases = res.Select(item => new PurchaseDTO()
                    {
                        Id = item.Id,
                        Amount = item.Amount,
                        Currency = item.Currency,
                        CurrencyName = _getCurrency(item.Currency).ToString(),
                        Name = _userInterface.GetUserById(item.UserId).Result.Name,
                        UserId = item.UserId
                    })
                    .ToList();

                return Ok(purchases);
            }
            catch (Exception e)
            {
                return BadRequest(new InternalServerError("The quotation was not found"));
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] PurchaseDTO purchase)
        {
            try
            {
                var purchaseEntity = new Purchase()
                {
                    Amount = purchase.Amount,
                    UserId = purchase.UserId,
                    Currency = purchase.Currency
                };
                await _purchaseRepository.Add(purchaseEntity);
                
                return Ok(purchase);
            }
            catch (Exception e)
            {
                return BadRequest(new InternalServerError("The quotation was not found"));
            }
        }
    }
}