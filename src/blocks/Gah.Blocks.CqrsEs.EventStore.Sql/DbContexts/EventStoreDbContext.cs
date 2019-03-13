namespace Gah.Blocks.CqrsEs.EventStore.Sql.DbContexts
{
    using Gah.Blocks.CqrsEs.EventStore.Sql.Options;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class <c>EventStoreDbContext</c>.
    /// </summary>
    public class EventStoreDbContext : EventStoreDbContext<EventStoreDbContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventStoreDbContext" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="storeOptions">The store options.</param>
        public EventStoreDbContext(
            DbContextOptions<EventStoreDbContext> options,
            EventStoreOptions storeOptions)
            : base(options, storeOptions)
        {
        }
    }
}
