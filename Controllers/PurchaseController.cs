using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quotation.API.Data;
using Quotation.API.DTOs;
using Quotation.API.Model;


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


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var res = await _purchaseRepository.GetPurchaseList();
            var purchases = new List<PurchaseDTO>();
            foreach (var item in res)
            {
                purchases.Add(new PurchaseDTO()
                {
                    Id = item.Id,
                    Amount = item.Amount,
                    Currency = item.Currency,
                    Name = item.Users.Name,
                    UserId = item.UserId
                });
            }

            return Ok(purchases);
        }
    }
}