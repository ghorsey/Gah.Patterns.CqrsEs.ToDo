namespace Gah.Blocks.CqrsEs.EventStore.Sql.DbContexts
{
    using System.Threading.Tasks;

    using Gah.Blocks.CqrsEs.EventStore.Sql.Entities;
    using Gah.Blocks.CqrsEs.EventStore.Sql.Extensions;
    using Gah.Blocks.CqrsEs.EventStore.Sql.Options;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class <c>EventStoreDbContext</c>.
    /// Implements the <see cref="Microsoft.EntityFrameworkCore.DbContext" />
    /// Implements the <see cref="Gah.Blocks.CqrsEs.EventStore.Sql.DbContexts.IEventStoreDbContext" />
    /// </summary>
    /// <typeparam name="TContext">The type of the t context.</typeparam>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    /// <seealso cref="Gah.Blocks.CqrsEs.EventStore.Sql.DbContexts.IEventStoreDbContext" />
    public class EventStoreDbContext<TContext> : DbContext, IEventStoreDbContext
        where TContext : DbContext, IEventStoreDbContext
    {
        /// <summary>
        /// The event store options
        /// </summary>
        private readonly EventStoreOptions eventStoreOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventStoreDbContext{TContext}"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="eventStoreOptions">The event store options.</param>
        public EventStoreDbContext(
            DbContextOptions<TContext> options,
            EventStoreOptions eventStoreOptions)
            : base(options)
        {
            this.eventStoreOptions = eventStoreOptions;
        }

        /// <summary>
        /// Gets or sets the event sources.
        /// </summary>
        /// <value>The event sources.</value>
        public DbSet<EventSource> EventSources { get; set; }

        /// <summary>
        /// Asynchronously saves the changes.
        /// </summary>
        /// <returns>A/an <c>Task&lt;System.Int32&gt;</c>.</returns>
        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.</remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureEventSourceContext(this.eventStoreOptions);
        }
    }
}
