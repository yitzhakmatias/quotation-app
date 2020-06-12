using System.Collections.Generic;

namespace Quotation.API.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double DollarLimit { get; set; }
        public double RealLimit { get; set; }

        public ICollection<Purchase> Purchases { get; set; }
    }
}