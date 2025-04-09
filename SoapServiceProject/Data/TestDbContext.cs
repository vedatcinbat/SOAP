using Microsoft.EntityFrameworkCore;
using SoapServiceProject.Data.Entities;

namespace SoapServiceProject.Data {
    public class TestDbContext : DbContext {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}

