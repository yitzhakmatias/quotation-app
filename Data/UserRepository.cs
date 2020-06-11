using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quotation.API.DataContext;
using Quotation.API.Model;

namespace Quotation.API.Data
{
    public class UserRepository : IUserInterface
    {
        private readonly QuotationContext _quotationContext;

        public UserRepository(QuotationContext quotationContext)
        {
            _quotationContext = quotationContext;
        }
        public async Task<User> GetUserById(int id)
        {
            var response = await _quotationContext.Users.FirstOrDefaultAsync(p=>p.Id==id);
            return response;
        }
    }
}