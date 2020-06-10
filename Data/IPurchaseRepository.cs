using System.Collections.Generic;
using System.Threading.Tasks;
using Quotation.API.Model;

namespace Quotation.API.Data
{
    public interface IPurchaseRepository
    {
        Task Add(Purchase purchase);
        Task<List<Purchase>> GetPurchaseList();
    }
}