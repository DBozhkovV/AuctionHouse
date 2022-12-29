using AuctionHouse.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionHouse.Data
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration configuration;

        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new DataSeeder(modelBuilder).Seed();
        }


        /*
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(configuration.GetConnectionString("PostgresDatabase"));
        }
        */

    }
}
