using AuctionHouse.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionHouse.Data
{
    public class DataSeeder
    {
        private readonly ModelBuilder modelBuilder;
        
        public DataSeeder(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed() 
        {   
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Admin",
                    LastName = "Admin",
                    Username = "admin",
                    Email = "admin@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                    PhoneNumber = "admin",
                    Role = Role.Admin,
                    IsVerified = true
                });
        }
    }
}