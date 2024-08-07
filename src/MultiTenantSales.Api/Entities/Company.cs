namespace MultiTenantSales.Api.Entities;

public class Company
{
    public int Id { get; set; }

    public int TenantId { get; set; }

    public string Name { get; set; } = null!;
}
