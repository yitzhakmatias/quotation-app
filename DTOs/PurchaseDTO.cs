using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Quotation.API.Model;

namespace Quotation.API.DTOs
{
    public class PurchaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int UserId { get; set; }
        public double Amount { get; set; }   
        [JsonConverter(typeof(StringEnumConverter))]
        public int Currency { get; set; }

        public string CurrencyName { get; set; }
    }
}