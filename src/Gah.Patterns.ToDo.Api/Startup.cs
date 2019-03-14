namespace Gah.Patterns.ToDo.Api
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Gah.Blocks.CqrsEs.Commands;
    using Gah.Blocks.CqrsEs.Events;
    using Gah.Blocks.CqrsEs.EventStore.Sql.Configuration;
    using Gah.Blocks.CqrsEs.Queries;
    using Gah.Patterns.ToDo.Commands;
    using Gah.Patterns.ToDo.Commands.Items;
    using Gah.Patterns.ToDo.Commands.Lists;
    using Gah.Patterns.ToDo.Events.Items;
    using Gah.Patterns.ToDo.Events.Lists;
    using Gah.Patterns.ToDo.Query.Domain;
    using Gah.Patterns.ToDo.Query.EventHandlers;
    using Gah.Patterns.ToDo.Query.Queries.Items;
    using Gah.Patterns.ToDo.Query.Queries.Lists;
    using Gah.Patterns.ToDo.Query.QueryHandlers;
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
                            b.MigrationsAssembly("Gah.Patterns.ToDo.Query.Repository.Sql");
                            b.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                        }));

            // add ef core event store
            services.AddEventStore(
                options => options.ConfigureDbContext = db => db.UseSqlServer(
                               ConnectionString,
                               b =>
                                   {
                                       b.MigrationsAssembly("Gah.Patterns.ToDo.Api");
                                       b.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                                   }));

            // fix an issue with version 3.1.2 of Microsoft.AspNetCore.Mvc.Versioning that does not support the latest routing strategy
            services.AddMvc(options => options.EnableEndpointRouting = false)
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddApiVersioning(
                options =>
                {
                    options.ReportApiVersions = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                });

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");

            services.AddSwagger();

            // Add mediator
            services.AddScoped<IMediator, Mediator>();
            services.AddTransient<ServiceFactory>(sp => sp.GetService);

            // Add Repositories
            services.AddScoped<IToDoListRepository, ToDoListRepository>();
            services.AddScoped<IToDoItemRepository, ToDoItemRepository>();

            // Add CQRS
            services.AddScoped<IQueryBus, QueryBus>();
            services.AddScoped<ICommandBus, CommandBus>();
            services.AddScoped<IEventBus, EventBus>();

            // Query Handlers
            // List Query Handlers
            services.AddScoped<IRequestHandler<FindAllListsQuery, List<ToDoList>>, ListsQueryHandler>();
            services.AddScoped<IRequestHandler<FindListQuery, ToDoList>, ListsQueryHandler>();

            // Item Query Handler
            services
                .AddScoped<IRequestHandler<FindAllItemsQuery, List<ToDoItem>>, ItemsQueryHandler>();
            services.AddScoped<IRequestHandler<FindItemQuery, ToDoItem>, ItemsQueryHandler>();

            // Command Handlers
            services.AddScoped<IRequestHandler<UpdateListCommand, Unit>, ListCommandHandler>();
            services.AddScoped<IRequestHandler<CreateListCommand, Unit>, ListCommandHandler>();
            services.AddScoped<IRequestHandler<CreateItemCommand, Unit>, ListCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateItemCommand, Unit>, ListCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateItemIsDoneCommand, Unit>, ListCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteItemCommand, Unit>, ListCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteListCommand, Unit>, ListCommandHandler>();

            // Event Handlers
            // List Event Handlers
            services.AddScoped<INotificationHandler<ListCreatedEvent>, ListEventHandlers>();
            services.AddScoped<INotificationHandler<ListUpdatedEvent>, ListEventHandlers>();
            services.AddScoped<INotificationHandler<ListCountsChangedEvent>, ListEventHandlers>();
            services.AddScoped<INotificationHandler<ListDeletedEvent>, ListEventHandlers>();

            // Item Event Handlers
            services.AddScoped<INotificationHandler<ItemAddedEvent>, ItemEventHandlers>();
            services.AddScoped<INotificationHandler<ItemUpdatedEvent>, ItemEventHandlers>();
            services.AddScoped<INotificationHandler<ItemIsDoneUpdatedEvent>, ItemEventHandlers>();
            services.AddScoped<INotificationHandler<ItemDeletedEvent>, ItemEventHandlers>();
            services.AddScoped<INotificationHandler<ListDeletedEvent>, ItemEventHandlers>();
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