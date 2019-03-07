namespace Gah.Patterns.Todo.Repository.Sql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Gah.Patterns.Todo.Domain;
    using Gah.Patterns.Todo.Repository.Sql.Data;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Basic Class
    /// </summary>
    public class ToDoListRepository : ITodoListRepository
    {
        /// <summary>
        /// The entities
        /// </summary>
        private readonly DbSet<ToDoList> entities;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoListRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        public ToDoListRepository(ApplicationDbContext context, ILogger logger)
        {
            this.context = context;
            this.logger = logger;
            this.entities = context.ToDoLists;
        }

        /// <summary>
        /// Finds the items.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns>A/an <c>Task&lt;List&lt;System.Int32&gt;&gt;</c>.</returns>
        public Task<List<ToDoList>> FindListsAsync(string title = "")
        {
            this.logger.LogDebug("Finding lists with title matching '{title}'", title);
            var q = this.entities.AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
            {
                q = q.Where(_ => _.Title.Contains(title));
            }

            return q.ToListAsync();
        }

        /// <summary>
        /// Finds the list.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A/an <c>Task&lt;ToDoList&gt;</c>.</returns>
        public Task<ToDoList> FindListAsync(Guid id)
        {
            this.logger.LogDebug("Finding list: {id}", id);
            return this.entities.FirstOrDefaultAsync(_ => _.Id == id);
        }

        /// <summary>
        /// Creates the list.
        /// </summary>
        /// <param name="toDoList">To do list.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        public async Task CreateListAsync(ToDoList toDoList)
        {
            this.logger.LogDebug("Creating the task {@list}", toDoList);
            await this.entities.AddAsync(toDoList);

            await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the list.
        /// </summary>
        /// <param name="toDoList">To do list.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        public async Task UpdateListAsync(ToDoList toDoList)
        {
            this.logger.LogDebug("Updating the task {@list}", toDoList);

            var toDelete = await this.FindListAsync(toDoList.Id);
            this.entities.Remove(toDelete);

            await this.entities.AddAsync(toDoList);

            await this.context.SaveChangesAsync();
        }
    }
}
