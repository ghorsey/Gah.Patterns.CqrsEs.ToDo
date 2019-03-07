namespace Gah.Patterns.ToDo.Repository.Sql.Data
{
    using System.Threading.Tasks;

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
            await context.Database.EnsureCreatedAsync();
        }
    }
}