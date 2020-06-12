using System.Collections.Generic;
using System.Linq;
using Quotation.API.Model;
using RandomTestValues;

namespace QuotationTest
{
    public static class FakePurchaseData
    {
        public static List<Purchase> GetFakeData()
        {
            var i = 1;
            var purchases = RandomValue.List<Purchase>();
         
            return purchases;
        }
    }
}