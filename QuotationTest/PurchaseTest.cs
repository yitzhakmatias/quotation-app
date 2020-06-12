using System.Threading.Tasks;
using QuotationTest.Base;
using Xunit;

namespace QuotationTest
{
    public class PurchaseTest : BaseEndpointTests, IClassFixture<WebApplicationFactoryWithSqlite>
    {
        public PurchaseTest(WebApplicationFactoryWithSqlite factory) : base(factory)
        {
        }

        [Theory]
        [MemberData(nameof(Endpoints))]
        public async Task Get_Should_Return_Forecast(string url)
        {
            const string expectedContentType = "application/json; charset=utf-8";
            var client = Factory.CreateClient();

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal(expectedContentType,
                response.Content.Headers.ContentType.ToString());
        }
        
    }
}