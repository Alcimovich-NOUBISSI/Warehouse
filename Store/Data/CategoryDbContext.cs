using Microsoft.EntityFrameworkCore;
using Store.Models;

namespace Store.Data
{
    public class CategoryDbContext: DbContext
    {
        public CategoryDbContext(DbContextOptions<CategoryDbContext> options): base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Provider_Category> Provider_Category { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Provider_Category>()
                .HasKey(p_c => new { p_c.CategoryId, p_c.ProviderId });
        }
    }
}
