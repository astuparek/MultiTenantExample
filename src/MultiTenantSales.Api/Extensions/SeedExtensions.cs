using MultiTenantSales.Api.Data;
using MultiTenantSales.Api.Entities;

namespace MultiTenantSales.Api.Extensions;

public static class SeedExtensions
{
    public static void SeedDatabase(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using ApplicationDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        foreach (var tenantId in Tenants.All)
        {
            var company = new Company
            {
                TenantId = tenantId,
                Name = $"Company {tenantId}"
            };

            dbContext.Add(company);

            for (var i = 0; i < 10_000; i++)
            {
                var sale = new Sale
                {
                    TenantId = tenantId,
                    Company = company,
                    Amount = Random.Shared.Next(100, 10_000),
                    CreatedOnUtc = DateTime.UtcNow.AddDays(-Random.Shared.Next(100, 1_000))
                };

                dbContext.Add(sale);
            }
        }

        dbContext.SaveChanges();
    }
}
