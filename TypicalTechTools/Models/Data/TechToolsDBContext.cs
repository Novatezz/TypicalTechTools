using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
namespace TypicalTechTools.Models.Data
{
    public class TechToolsDBContext: DbContext
    {
        //Constructor accepting DbContextOptions and passing it to the base class 
        public TechToolsDBContext(DbContextOptions options):base(options) 
        {

        }

        // Define DbSets for the entities in the database
        public DbSet<Product> Products { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Seed initial data for the AdminUser table
            builder.Entity<AdminUser>().HasData(
                new AdminUser
                {
                    Id = 1,
                    UserName = "Alex",
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword("Password_1"),
                    Email = "Test@email.com"
                });

            // Seed initial data for the Product table
            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Code = 12345,
                    Name = "Generic Headphones",
                    Price = 84.99,
                    Description = "bluetooth headphones with fair battery life and a 1 month warranty"
                },
                new Product
                {
                    Id = 2,
                    Code = 12346,
                    Name = "Expensive Headphones",
                    Price = 149.99,
                    Description = "bluetooth headphones with good battery life and a 6 month warranty"
                },
                new Product
                {
                    Id = 3,
                    Code = 12347,
                    Name = "Name Brand Headphones",
                    Price = 199.99,
                    Description = "bluetooth headphones with good battery life and a 12 month warranty"
                }, new Product
                {
                    Id = 4,
                    Code = 12348,
                    Name = "Generic Wireless Mouse",
                    Price = 39.99,
                    Description = "simple bluetooth pointing device"
                }, new Product
                {
                    Id = 5,
                    Code = 12349,
                    Name = "Logitach Mouse and Keyboard",
                    Price = 73.99,
                    Description = "mouse and keyboard wired combination"
                }, new Product
                {
                    Id = 6,
                    Code = 12350,
                    Name = "Logitach Wireless Mouse",
                    Price = 149.99,
                    Description = "quality wireless mouse"
                });

            // Seed initial data for the Comment table
            builder.Entity<Comment>().HasData(
                new Comment 
                {
                    Id = 1, 
                    Text = "This is a great product. Highly Recommended.", 
                    Code = 12345 
                },
                new Comment 
                { 
                    Id = 2, 
                    Text = "Not worth the excessive price. Stick with a cheaper generic one.", 
                    Code = 12350 
                },
                new Comment 
                { 
                    Id = 3, 
                    Text = "A great budget buy. As good as some of the expensive alternatives.", 
                    Code = 12345 
                },
                new Comment 
                { 
                    Id = 4, 
                    Text = "Total garbage. Never buying this brand again.",
                    Code = 12347 
                });

        }
    }
}
