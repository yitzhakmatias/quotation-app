using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quotation.API.DataContext;
using QuotationTest.Base;

namespace QuotationTest
{
    public class WebApplicationFactoryWithSqlite : BaseWebApplicationFactory<Quotation.API.Startup>
    {
        private readonly string _connectionString = $"Data Source={AppDomain.CurrentDomain.SetupInformation.ApplicationBase}QuotationDb.db";

        protected override void ConfigureWebHost(IWebHostBuilder builder) =>
            builder.ConfigureServices(services =>
            {
                services
                    .AddEntityFrameworkSqlite()
                    .AddDbContext<QuotationContext>(options =>
                    {
                        options.UseSqlite(_connectionString);
                        options.UseInternalServiceProvider(services.BuildServiceProvider());
                    });
            });
    }
}