namespace Gah.Patterns.ToDo.Api
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using Gah.Blocks.CqrsEs.EventStore.Sql.DbContexts;
    using Gah.Patterns.ToDo.Api.Data;
    using Gah.Patterns.ToDo.Query.Repository.Sql.Data;

    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Serilog;
    using Serilog.Events;

    /// <summary>
    /// The entry program of the application
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Program
    {
        /// <summary>
        /// The entry method of the program
        /// </summary>
        /// <param name="args">The arguments</param>
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var eventStoreContext = services.GetRequiredService<EventStoreDbContext>();

                    DbInitializer.InitializeAsync(context).GetAwaiter().GetResult();
                    DbInitializer.InitializeAsync(eventStoreContext).GetAwaiter().GetResult();
                }
                catch (Exception x)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(x, "An error occurred while seeding the database");
                }
            }

            host.Run();
        }

        /// <summary>
        /// Creates the Web Host Builder
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <returns><see cref="IWebHostBuilder"/></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog(
                    (context, configuration) =>
                        {
                            configuration.MinimumLevel.Debug()
                                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                                .MinimumLevel.Override("System", LogEventLevel.Warning)
                                .Enrich.FromLogContext()
                                .WriteTo.Console(
                                    outputTemplate:
                                    "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}");
                        })
                .UseStartup<Startup>();
    }
}
