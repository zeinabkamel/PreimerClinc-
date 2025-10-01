using PreimerClinc.Data;
using PreimerClinc.logger;
using Serilog;
using Serilog.Events;
using System;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Tracing;

namespace PreimerClinc;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateBootstrapLogger();

        try
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog((context, services, loggerConfiguration) =>
                {
                    if (IsMigrateDatabase(args))
                    {
                        loggerConfiguration
                            .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
                            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                            .WriteTo.Async(c => c.Console(standardErrorFromLevel: LogEventLevel.Error));
                    }
                    else
                    {
                        loggerConfiguration
                        #if DEBUG
                            .MinimumLevel.Debug()
                        #else
                            .MinimumLevel.Information()
                        #endif
                            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                            .Enrich.With(new UserTenantCorrelationEnricher(
                             services.GetRequiredService<IHttpContextAccessor>(),
                             services.GetRequiredService<ICurrentTenant>(),
                             services.GetRequiredService<ICorrelationIdProvider>()
                              ))
                                  .WriteTo.Async(c => c.File("Logs/logs.txt",
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} " +
                                "(UserId={UserId}, UserName={UserName}, TenantId={TenantId}, CorrelationId={CorrelationId}){NewLine}{Exception}"))
            .WriteTo.Async(c => c.Console(
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} " +
                                "(UserId={UserId}, UserName={UserName}, TenantId={TenantId}, CorrelationId={CorrelationId}){NewLine}{Exception}"))

                            .WriteTo.Async(c => c.AbpStudio(services));
                    }
                });
            if (IsMigrateDatabase(args))
            {
                builder.Services.AddDataMigrationEnvironment();
            }
            await builder.AddApplicationAsync<PreimerClincModule>();
            var app = builder.Build();
            await app.InitializeApplicationAsync();

            if (IsMigrateDatabase(args))
            {
                await app.Services.GetRequiredService<PreimerClincDbMigrationService>().MigrateAsync();
                var previous = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Migration completed.");
                Console.ForegroundColor = previous;
                return 0;
            }

            Log.Information("Starting PreimerClinc.");
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            if (ex is HostAbortedException)
            {
                throw;
            }

            Log.Fatal(ex, "PreimerClinc terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static bool IsMigrateDatabase(string[] args)
    {
        return args.Any(x => x.Contains("--migrate-database", StringComparison.OrdinalIgnoreCase));
    }
}
