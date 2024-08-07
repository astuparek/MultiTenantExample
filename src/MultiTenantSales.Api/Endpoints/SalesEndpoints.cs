using Microsoft.EntityFrameworkCore;
using MultiTenantSales.Api.Data;

namespace MultiTenantSales.Api.Endpoints;

public static class SalesEndpoints
{
    public static void MapSalesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/sales", async (
            int companyId,
            ApplicationDbContext context,
            int page = 1,
            int pageSize = 10) =>
        {
            var sales = await context.Sales
                .Include(s => s.Company)
                .Where(s => s.CompanyId == companyId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Results.Ok(sales);
        });
    }
}
