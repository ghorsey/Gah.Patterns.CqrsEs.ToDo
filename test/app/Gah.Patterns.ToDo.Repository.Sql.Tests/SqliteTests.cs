namespace Gah.Patterns.ToDo.Repository.Sql.Tests
{
    using System;
    using System.Threading.Tasks;

    using Gah.Patterns.Todo.Repository.Sql.Data;

    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class <c>SqliteTests</c>.
    /// </summary>
    public abstract class SqliteTests
    {
        /// <summary>
        /// run test as an asynchronous operation.
        /// </summary>
        /// <param name="runner">The runner.</param>
        /// <param name="setup">The setup.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        protected virtual async Task RunTestAsync(Func<ApplicationDbContext, Task> runner, Func<ApplicationDbContext, Task> setup = null)
        {
            var connection = new SqliteConnection("Datasource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var context = new ApplicationDbContext(options))
                {
                    await context.Database.EnsureCreatedAsync();
                }

                if (setup != null)
                {
                    using (var context = new ApplicationDbContext(options))
                    {
                        await setup(context);
                    }
                }

                using (var context = new ApplicationDbContext(options))
                {
                    await runner(context);
                }
            }
            catch (Exception x)
            {
                Console.WriteLine(x.ToString());
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
