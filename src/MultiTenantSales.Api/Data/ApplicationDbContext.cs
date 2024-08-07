using Microsoft.EntityFrameworkCore;
using MultiTenantSales.Api.Entities;
using MultiTenantSales.Api.Services;

namespace MultiTenantSales.Api.Data;

public class ApplicationDbContext : DbContext
{
    private readonly TenantProvider _tenantProvider;
    private readonly int _tenantId;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        TenantProvider tenantProvider)
        : base(options)
    {
        _tenantProvider = tenantProvider;
        _tenantId = tenantProvider.GetTenantId();
    }

    public DbSet<Company> Companies { get; set; }

    public DbSet<Sale> Sales { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_tenantProvider.GetConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(builder => {
            builder.HasIndex(c => c.TenantId);

            builder.HasQueryFilter(c => c.TenantId == _tenantId);
        });

        modelBuilder.Entity<Sale>(builder => {
            builder.HasIndex(c => c.TenantId);

            builder.HasQueryFilter(c => c.TenantId == _tenantId);
        });
    }
}
