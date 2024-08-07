using Microsoft.EntityFrameworkCore;
using MultiTenantSales.Api.Data;
using MultiTenantSales.Api.Endpoints;
using MultiTenantSales.Api.Extensions;
using MultiTenantSales.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TenantProvider>();

builder.Services.Configure<TenantConnectionStrings>(options =>
    builder.Configuration.GetSection(nameof(TenantConnectionStrings)).Bind(options));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    
    //app.ApplyMigrations();
    //app.SeedDatabase();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapCompanyEndpoints();
app.MapSalesEndpoints();

app.Run();