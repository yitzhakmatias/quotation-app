using System.Threading.Tasks;
using Quotation.API.Model;

namespace Quotation.API.Data
{
    public interface IQuotationRepository
    {
        Task<Model.Quotation> GetQuotation(Currencies? currency);
    }
}