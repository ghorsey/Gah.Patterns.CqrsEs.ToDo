namespace Gah.Blocks.CqrsEs.EventStore.Sql.Configuration
{
    using System;

    using Gah.Blocks.CqrsEs.EventStore.Sql.DbContexts;
    using Gah.Blocks.CqrsEs.EventStore.Sql.Options;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Class <c>EventStoreBuilderExtensions</c>.
    /// </summary>
    public static class EventStoreBuilderExtensions
    {
        /// <summary>
        /// Adds the event store.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="optionsAction">The options action.</param>
        /// <returns>A/an <c>IServiceCollection</c>.</returns>
        public static IServiceCollection AddEventStore(
            this IServiceCollection serviceCollection,
            Action<EventStoreOptions> optionsAction = null) =>
            serviceCollection.AddEventStore<EventStoreDbContext>(optionsAction);

        /// <summary>
        /// Adds the event store.
        /// </summary>
        /// <typeparam name="TContext">The type of the t context.</typeparam>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="optionsAction">The options action.</param>
        /// <returns>A/an <c>IServiceCollection</c>.</returns>
        public static IServiceCollection AddEventStore<TContext>(
            this IServiceCollection serviceCollection,
            Action<EventStoreOptions> optionsAction = null)
            where TContext : DbContext, IEventStoreDbContext
        {
            serviceCollection.AddEventStoreDbContext<TContext>(optionsAction);

            serviceCollection.AddScoped<IEventStore, EventStore>();
            return serviceCollection;
        }

        /// <summary>
        /// Adds the event store database context.
        /// </summary>
        /// <typeparam name="TContext">The type of the t context.</typeparam>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="optionsActions">The options actions.</param>
        /// <returns>A/an <c>IServiceCollection</c>.</returns>
        public static IServiceCollection AddEventStoreDbContext<TContext>(
            this IServiceCollection serviceCollection,
            Action<EventStoreOptions> optionsActions = null)
            where TContext : DbContext, IEventStoreDbContext
        {
            var options = new EventStoreOptions();
            serviceCollection.AddSingleton(options);

            optionsActions?.Invoke(options);

            if (options.ResolveDbContextOptions != null)
            {
                serviceCollection.AddDbContext<TContext>(options.ResolveDbContextOptions);
            }
            else
            {
                serviceCollection.AddDbContext<TContext>(
                    contextBuilder => options.ConfigureDbContext?.Invoke(contextBuilder));
            }

            serviceCollection.AddScoped<IEventStoreDbContext, TContext>();

            return serviceCollection;
        }
    }
}