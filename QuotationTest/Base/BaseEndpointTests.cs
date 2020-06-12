using System.Collections.Generic;

namespace QuotationTest.Base
{
    public abstract class BaseEndpointTests
    {
        protected BaseWebApplicationFactory<Quotation.API.Startup> Factory { get; }

        protected BaseEndpointTests(BaseWebApplicationFactory<Quotation.API.Startup> factory) =>
            Factory = factory;
        public static readonly IEnumerable<object[]> Endpoints = new List<object[]>()
        {
            new object[] {"/api/purchase/"},
            new object[] {"/api/user"},
          
        };

    }
}