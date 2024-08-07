using Microsoft.Extensions.Options;
using MultiTenantSales.Api.Entities;

namespace MultiTenantSales.Api.Services;

public sealed class TenantProvider
{
    private const string TenantIdHeaderName = "X-TenantId";

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly TenantConnectionStrings _connectionStrings;

    public TenantProvider(
        IHttpContextAccessor httpContextAccessor,
        IOptions<TenantConnectionStrings> connectionStrings)
    {
        _httpContextAccessor = httpContextAccessor;
        _connectionStrings = connectionStrings.Value;
    }

    public int GetTenantId()
    {
        var tenantIdHeader = _httpContextAccessor.HttpContext?
            .Request
            .Headers[TenantIdHeaderName];

        if (!tenantIdHeader.HasValue ||
            !int.TryParse(tenantIdHeader.Value, out int tenantId) ||
            !Tenants.All.Contains(tenantId))
        {
            throw new ApplicationException("Tenant ID is not present");
        }

        return tenantId;
    }

    public string GetConnectionString()
    {
        return _connectionStrings.Values[GetTenantId()];
    }
}
