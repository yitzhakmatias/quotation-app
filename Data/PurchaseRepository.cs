using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quotation.API.DataContext;
using Quotation.API.Model;

namespace Quotation.API.Data
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly QuotationContext _quotationContext;

        public PurchaseRepository(QuotationContext quotationContext)
        {
            _quotationContext = quotationContext;
        }

        public async Task Add(Purchase purchase)
        {
            await _quotationContext.Purchases.AddAsync(purchase).AsTask();
            await _quotationContext.SaveChangesAsync();
        }

        public async Task<List<Purchase>> GetPurchaseList()
        {
            var response = await _quotationContext.Purchases.ToListAsync();
            return response;
        }
    }
}