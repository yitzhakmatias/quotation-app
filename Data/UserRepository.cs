using System.Threading.Tasks;
using Quotation.API.Model;

namespace Quotation.API.Data
{
    public class UserRepository : IUserInterface
    {
        public Task<User> GetUserById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}