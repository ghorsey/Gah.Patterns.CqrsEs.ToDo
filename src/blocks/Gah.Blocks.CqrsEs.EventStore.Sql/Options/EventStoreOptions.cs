namespace Gah.Blocks.CqrsEs.EventStore.Sql.Options
{
    using System;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class <c>EventStoreOptions</c>.
    /// </summary>
    public class EventStoreOptions
    {
        /// <summary>
        /// Gets or sets the configure database context.
        /// </summary>
        /// <value>The configure database context.</value>
        public Action<DbContextOptionsBuilder> ConfigureDbContext { get; set; }

        /// <summary>
        /// Gets or sets the resolve database context options.
        /// </summary>
        /// <value>The resolve database context options.</value>
        public Action<IServiceProvider, DbContextOptionsBuilder> ResolveDbContextOptions { get; set; }

        /// <summary>
        /// Gets or sets the default schema.
        /// </summary>
        /// <value>The default schema.</value>
        public string DefaultSchema { get; set; } = null;

        /// <summary>
        /// Gets or sets the event store.
        /// </summary>
        /// <value>The event store.</value>
        public TableConfiguration EventStore { get; set; } = new TableConfiguration("ES_EventStore");
    }
}