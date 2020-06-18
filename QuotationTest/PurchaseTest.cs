using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Quotation.API.DTOs;
using Quotation.API.Model;
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

        [Theory]
       
        [InlineData(1, 10.5,"/api/purchase/register")]
        public async Task When_Register_Purchase_HTTP_STATUS_ERROR(int userId, double amount,string url)
        {
            var purchase = new  {UserId = userId, Currency = 2, Amount = amount};


            var json = JsonConvert.SerializeObject(purchase) ;
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using  var client = Factory.CreateClient();

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
            
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
           
        }
    }
}