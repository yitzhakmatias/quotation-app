using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly IQuotationRepository _quotationRepository;
        private readonly ILogger<PurchaseController> _logger;

        public PurchaseController(IPurchaseRepository purchaseRepository, IUserInterface userInterface,
            IQuotationRepository quotationRepository, ILogger<PurchaseController> logger)
        {
            _purchaseRepository = purchaseRepository;
            _userInterface = userInterface;
            _quotationRepository = quotationRepository;
            _logger = logger;
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

        [HttpGet("currency")]
        public async Task<IActionResult> GetCurrency()
        {
            return Ok(new List<CurrencyDto>()
            {
                new CurrencyDto() {Id = 1, Name = "Dollar"}, new CurrencyDto() {Id = 2, Name = "Real"},
            });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var res = await _purchaseRepository.GetPurchaseList();
                if (res == null) return Ok(null);

                var purchases = res.Select(item => new PurchaseDTO()
                    {
                        Id = item.Id,
                        Amount = item.Amount,
                        Currency = item.Currency,
                        CurrencyName = _getCurrency(item.Currency).ToString(),
                        Name = _userInterface.GetUserById(item.UserId).Result.Name,
                        UserId = item.UserId,
                        Value = Math.Round(item.Value, 2)
                    })
                    .ToList();

                return Ok(purchases);
            }
            catch (Exception e)
            {
                _logger.LogError("error: " + e.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] PurchaseDTO purchase)
        {
            try
            {
                _logger.LogInformation("register purchase of currency");

                Currencies currencyVal = _getCurrency(purchase.Currency);
                var quotation = await _quotationRepository.GetQuotation(currencyVal);

                var value = await UpdateAmount(purchase.Amount, quotation, currencyVal);

                if (!CheckAmountLimits(value, currencyVal, purchase.UserId))
                {
                    return BadRequest(new InternalServerError("El limite de compra se ha excedido"));
                }

                var purchaseEntity = new Purchase()
                {
                    Amount = purchase.Amount,
                    Value = value,
                    UserId = purchase.UserId,
                    Currency = purchase.Currency
                };
                await _purchaseRepository.Add(purchaseEntity);

                return Ok(purchaseEntity);
            }
            catch (Exception e)
            {
                _logger.LogError("error: " + e.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        private async Task<double> UpdateAmount(double purchaseAmount, Model.Quotation quotation,
            Currencies currencyVal)
        {
            switch (currencyVal)
            {
                case Currencies.Dollar:
                {
                    return purchaseAmount / quotation.Compra;
                }
                case Currencies.Real:
                {
                    return purchaseAmount / quotation.Compra;
                }
                default: return 0;
            }
        }

        private bool CheckAmountLimits(double amount, Currencies currency, int userId)
        {
            var user = _userInterface.GetUserById(userId).Result;
            switch (currency)
            {
                case Currencies.Dollar when amount > user.DollarLimit:
                case Currencies.Real when amount > user.RealLimit:
                    return false;
                default:
                    return true;
            }
        }
    }
}