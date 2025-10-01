namespace PreimerClinc.logger
{
    using Microsoft.AspNetCore.Http;
    using Serilog.Core;
    using Serilog.Events;
    using Volo.Abp.AspNetCore.Auditing;
    using Volo.Abp.MultiTenancy;
    using Volo.Abp.Security.Claims;
    using Volo.Abp.Tracing;
    /// <summary>
    /// enricher for configurring sirilog with tenant and user level
    /// </summary>

    public class UserTenantCorrelationEnricher : ILogEventEnricher
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICurrentTenant _currentTenant;
        private readonly ICorrelationIdProvider _correlationIdProvider;

        public UserTenantCorrelationEnricher(
            IHttpContextAccessor httpContextAccessor,
            ICurrentTenant currentTenant,
            ICorrelationIdProvider correlationIdProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _currentTenant = currentTenant;
            _correlationIdProvider = correlationIdProvider;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var user = httpContext?.User;

            // 🔹 UserId
            var userId =
                user?.FindFirst("sub")?.Value ??
                user?.FindFirst(AbpClaimTypes.UserId)?.Value ??
                "Anonymous";

            // 🔹 UserName
            var userName =
                user?.FindFirst("name")?.Value ??
                user?.FindFirst("unique_name")?.Value ??
                user?.FindFirst(AbpClaimTypes.UserName)?.Value ??
                "Anonymous";

            // 🔹 TenantId (من ICurrentTenant أو من claim)
            var tenantId =
                _currentTenant?.Id?.ToString() ??
                user?.FindFirst(AbpClaimTypes.TenantId)?.Value ??
                "Host";

            // 🔹 CorrelationId
            var correlationId =
                _correlationIdProvider.Get() ??
                httpContext?.TraceIdentifier ??
                "N/A";

            // Add properties
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("UserId", userId));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("UserName", userName));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("TenantId", tenantId));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("CorrelationId", correlationId));
        }
    }


}
