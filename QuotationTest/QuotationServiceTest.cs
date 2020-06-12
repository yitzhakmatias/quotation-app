using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Quotation.API.Model;
using Xunit;

namespace QuotationTest
{
    public class QuotationServiceTest : IClassFixture<WebApplicationFactory<Quotation.API.Startup>>
    {
        private HttpClient _client { get; }

        public QuotationServiceTest(WebApplicationFactory<Quotation.API.Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        [Theory]
        [InlineData(Currencies.Dollar)]
        public async Task Get_Quotation_Should_Be_Dollar(Currencies currency)
        {
            // Arrange
            var response =
                await _client.GetAsync("/api/quotation/currency?currency=" + currency);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            // Act
            var forecast =
                JsonConvert.DeserializeObject<Quotation.API.Model.Quotation>(
                    await response.Content.ReadAsStringAsync());
            // Assert
            forecast.Moneda.Should().Be(Currencies.Dollar.ToString());
        }

        [Theory]
        [InlineData(Currencies.Real)]
        public async Task Get_Quotation_Should_Be_Real(Currencies currency)
        {
            // Arrange
            var response =
                await _client.GetAsync("/api/quotation/currency?currency=" + currency);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            // Act
            var forecast =
                JsonConvert.DeserializeObject<Quotation.API.Model.Quotation>(
                    await response.Content.ReadAsStringAsync());
            // Assert
            forecast.Moneda.Should().Be(Currencies.Real.ToString());
        }

        [Theory]
        [InlineData("/api/quotation/currency?currency=1")]
        [InlineData("/api/quotation/currency?currency=2")]
        public async Task Smoketest_Should_ResultInOK(string endpoint)
        {
            var response = await _client.GetAsync(endpoint);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("/api/quotation")]
        public async Task Smoketest_Should_ResultInternalServerError(string endpoint)
        {
            var response = await _client.GetAsync(endpoint);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }
     
    }
}