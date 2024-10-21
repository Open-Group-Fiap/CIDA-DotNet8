using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CIDA.Api.Configuration.HealthChecks;

public static class HealthCheck
{
    public static void ConfigureHealthChecks(this IServiceCollection services,
        IConfiguration configuration)
    {
        var azureConnection = configuration.GetConnectionString("AzureConnection") ?? string.Empty;
        
        services.AddHealthChecks().AddSqlServer(
            azureConnection,
            name: "CidaDb-check",
            healthQuery: "SELECT 1;",
            failureStatus: HealthStatus.Degraded,
            tags: new[] { "db", "sql", "cida" });
        


        services.AddHealthChecksUI(opt =>
        {
            opt.SetEvaluationTimeInSeconds(240);
            opt.MaximumHistoryEntriesPerEndpoint(60);
            opt.SetApiMaxActiveRequests(1);
            opt.AddHealthCheckEndpoint("feedback", "/api/health");
        }).AddInMemoryStorage();
    }
}