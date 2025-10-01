using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PreimerClinc.Data;

public class PreimerClincDbContextFactory : IDesignTimeDbContextFactory<PreimerClincDbContext>
{
    public PreimerClincDbContext CreateDbContext(string[] args)
    {
        PreimerClincGlobalFeatureConfigurator.Configure();
        PreimerClincModuleExtensionConfigurator.Configure();

        PreimerClincEfCoreEntityExtensionMappings.Configure();
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<PreimerClincDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new PreimerClincDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}