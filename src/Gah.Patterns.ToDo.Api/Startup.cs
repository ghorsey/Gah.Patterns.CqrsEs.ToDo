namespace Gah.Patterns.ToDo.Api
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using Gah.Blocks.CqrsEs.Queries;
    using Gah.Patterns.ToDo.Api.Models.Queries.Lists;
    using Gah.Patterns.ToDo.Query.Domain;
    using Gah.Patterns.ToDo.Query.Repository;
    using Gah.Patterns.ToDo.Query.Repository.Sql;
    using Gah.Patterns.ToDo.Query.Repository.Sql.Data;
    using MediatR;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;

    /// <summary>
    /// Class <c>Startup</c>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            const string ConnectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;database=SampleCQRSES;trusted_connection=yes;";

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(
                    ConnectionString,
                    b =>
                        {
                            b.MigrationsAssembly("Gah.Patterns.Todo.Repository.Sql");
                            b.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                        }));

            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddApiVersioning(
                options =>
                {
                    options.ReportApiVersions = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 1);
                });

            services.AddSwagger();

            // Add mediator
            services.AddScoped<IMediator, Mediator>();
            services.AddTransient<ServiceFactory>(sp => sp.GetService);

            // Add Repositories
            services.AddScoped<IToDoListRepository, ToDoListRepository>();
            services.AddScoped<IToDoItemRepository, ToDoItemRepository>();

            // Add CQRS
            services.AddScoped<IQueryBus, QueryBus>();

            services
                .AddScoped<IRequestHandler<FindAllListsQuery, List<ToDoList>>, ListsQueryHandler>();
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAppSwagger();

            app.UseApiVersioning();

            app.UseMvc();
        }
    }
}