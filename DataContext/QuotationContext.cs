using Microsoft.EntityFrameworkCore;
using Quotation.API.Model;

namespace Quotation.API.DataContext
{
    public class QuotationContext : DbContext
    {
        public QuotationContext(DbContextOptions<QuotationContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
    }
}