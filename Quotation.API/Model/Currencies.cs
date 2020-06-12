using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace Quotation.API.Model
{
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Currencies
    {
        Dollar = 1,
        Real = 2,
    }
}