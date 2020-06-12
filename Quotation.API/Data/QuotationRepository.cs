using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Quotation.API.Model;
using Quotation.API.Utilities;

namespace Quotation.API.Data
{
    public class QuotationRepository : IQuotationRepository
    {
        private HttpClient _httpClient;
        private readonly string _url = @"http://www.bancoprovincia.com.ar/Principal/Dolar";
        
        public async Task<Model.Quotation> GetQuotation(Currencies? currency)
        {
            using (_httpClient = new HttpClient())
            {
                var content = await _httpClient.GetStringAsync(_url);
                var response = JArray.Parse(content);

                switch (currency)
                {
                    case Currencies.Dollar:
                    {
                        return (new Model.Quotation()
                        {
                            Moneda = Currencies.Dollar.ToString(),
                            Compra = Convert.ToDouble(response[0]),
                            Venta = Convert.ToDouble(response[1]),
                            Fecha = response[2].ToString()
                        });
                    }
                    case Currencies.Real:
                    {
                        return  (new Model.Quotation()
                        {
                            Moneda = Currencies.Real.ToString(),
                            Compra = Convert.ToDouble(response[0]) / 4,
                            Venta = Convert.ToDouble(response[1]) / 4,
                            Fecha = response[2].ToString()
                        });
                    }
                    default:
                    {
                        return null;
                    }
                }
            }
        }
    }
}