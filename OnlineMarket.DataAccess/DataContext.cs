using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.Models;

namespace OnlineMarket.DataAccess
{
    public class DataContext : IdentityDbContext<SystemUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<ProductReview> Reviews { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}