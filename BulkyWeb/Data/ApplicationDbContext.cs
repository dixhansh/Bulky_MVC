using BulkyWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        }

        public DbSet<Category> categories { get; set; }

        //seeding data into categories table in database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Fiction", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Acton", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Sci-Fi", DisplayOrder = 3 }
                );    
        }
        

        
    }
}
