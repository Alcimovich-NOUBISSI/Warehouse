using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Areas.Identity.Data;

namespace Store.Data;

public class IdentityContext : IdentityDbContext<StoreUser>
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new StoreUserEntityConfiguration());
    }
}

public class StoreUserEntityConfiguration : IEntityTypeConfiguration<StoreUser>
{
    public void Configure(EntityTypeBuilder<StoreUser> builder)
    {
        builder.Property(u => u.FName).HasMaxLength(255);
        builder.Property(u => u.LName).HasMaxLength(255);
    }
}
