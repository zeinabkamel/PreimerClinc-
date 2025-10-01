using Volo.Abp.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace PreimerClinc.Data;

public class PreimerClincDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public PreimerClincDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        
        /* We intentionally resolving the PreimerClincDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<PreimerClincDbContext>()
            .Database
            .MigrateAsync();

    }
}
