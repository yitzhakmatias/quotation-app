using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Quotation.API.Model
{
    public class Purchase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public double Ammount { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Currency Currency { get; set; }
    }
}