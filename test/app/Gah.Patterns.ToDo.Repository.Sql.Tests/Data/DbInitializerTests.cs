namespace Gah.Patterns.ToDo.Repository.Sql.Tests.Data
{
    using System.Threading.Tasks;

    using Gah.Patterns.ToDo.Api.Data;
    using Gah.Patterns.ToDo.Query.Repository.Sql.Data;

    using Xunit;

    /// <summary>
    /// Class <c>DbInitializerTests</c>.
    /// </summary>
    public class DbInitializerTests : SqliteTests
    {
        /// <summary>
        /// Defines the test method TestInitializeDB.
        /// </summary>
        /// <returns>A/an <c>Task</c>.</returns>
        [Fact]
        public Task TestInitializeDb()
        {
            return this.RunTestAsync(DbInitializer.InitializeAsync);
        }
    }
}