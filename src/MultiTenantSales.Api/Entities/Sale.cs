namespace MultiTenantSales.Api.Entities;

public class Sale
{
    public int Id { get; set; }

    public int TenantId { get; set; }

    public int CompanyId { get; set; }

    public decimal Amount { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public Company Company { get; set; } = null!;
}
