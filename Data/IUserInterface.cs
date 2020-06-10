using System.Threading.Tasks;
using Quotation.API.Model;

namespace Quotation.API.Data
{
    public interface IUserInterface
    {
        Task<User> GetUserById(int id);
    }
}