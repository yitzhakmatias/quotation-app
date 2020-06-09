using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace Quotation.API.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Currency
    {
        Dollar = 1,
        Real = 2,
    }
}