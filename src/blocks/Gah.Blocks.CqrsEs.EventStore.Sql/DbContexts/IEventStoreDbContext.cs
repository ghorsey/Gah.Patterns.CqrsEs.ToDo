namespace Gah.Blocks.CqrsEs.EventStore.Sql.DbContexts
{
    using System;
    using System.Threading.Tasks;

    using Gah.Blocks.CqrsEs.EventStore.Sql.Entities;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Interface <c>IEventStoreDbContext</c>
    /// </summary>
    public interface IEventStoreDbContext : IDisposable
    {
        /// <summary>
        /// Gets or sets the event sources.
        /// </summary>
        /// <value>The event sources.</value>
        DbSet<EventSource> EventSources { get; set; }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns>A/an <c>System.Int32</c>.</returns>
        int SaveChanges();

        /// <summary>
        /// Asynchronously saves the changes.
        /// </summary>
        /// <returns>A/an <c>Task&lt;System.Int32&gt;</c>.</returns>
        Task<int> SaveChangesAsync();
    }
}