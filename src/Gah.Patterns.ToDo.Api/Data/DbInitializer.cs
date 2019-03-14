namespace Gah.Patterns.ToDo.Api.Data
{
    using System.Threading.Tasks;

    using Gah.Blocks.CqrsEs.EventStore.Sql.DbContexts;
    using Gah.Patterns.ToDo.Query.Repository.Sql.Data;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class <c>DbInitializer</c>.
    /// </summary>
    public static class DbInitializer
    {
        /// <summary>
        /// The initialize async.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task InitializeAsync(ApplicationDbContext context)
        {
            ////await context.Database.EnsureCreatedAsync();
            await context.Database.MigrateAsync();
        }

        /// <summary>
        /// initialize as an asynchronous operation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A/an <c>Task.</c></returns>
        public static async Task InitializeAsync(EventStoreDbContext context)
        {
            ////await context.Database.EnsureCreatedAsync();

            await context.Database.MigrateAsync();
        }
    }
}