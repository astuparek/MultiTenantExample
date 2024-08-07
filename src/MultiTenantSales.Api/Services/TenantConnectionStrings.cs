namespace MultiTenantSales.Api.Services;

public class TenantConnectionStrings
{
    public Dictionary<int, string> Values { get; set; } = new();
}
