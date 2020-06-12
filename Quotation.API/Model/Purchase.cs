using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Quotation.API.Model
{
    public class Purchase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public double Amount { get; set; }   
        public double Value { get; set; }   
        public int Currency { get; set; }
        public User Users { get; set; }
    }
}