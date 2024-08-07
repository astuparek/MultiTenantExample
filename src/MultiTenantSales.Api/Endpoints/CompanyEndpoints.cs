using Microsoft.EntityFrameworkCore;
using MultiTenantSales.Api.Data;

namespace MultiTenantSales.Api.Endpoints;

public static class CompanyEndpoints
{
    public static void MapCompanyEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/companies/{id}", async (int id, ApplicationDbContext context) =>
        {
            var company = await context.Companies.SingleOrDefaultAsync(c => c.Id == id);

            return company is not null ? Results.Ok(company) : Results.NotFound();
        });
    }
}
