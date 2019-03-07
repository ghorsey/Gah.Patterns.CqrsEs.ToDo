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
    /// Class <c>ToDoItemRepository</c>.
    /// </summary>
    public class ToDoItemRepository : IToDoItemRepository
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// The entities
        /// </summary>
        private readonly DbSet<ToDoItem> entities;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoItemRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        public ToDoItemRepository(ApplicationDbContext context, ILogger logger)
        {
            this.context = context;
            this.entities = context.ToDoItems;
            this.logger = logger;
        }

        /// <summary>
        /// Finds the items asynchronous.
        /// </summary>
        /// <param name="listId">The list identifier.</param>
        /// <returns>A/an <c>Task&lt;List&lt;ToDoItem&gt;&gt;</c>.</returns>
        public Task<List<ToDoItem>> FindItemsAsync(Guid listId)
        {
            this.logger.LogDebug("Finding items for list: {listId}", listId);
            return this.entities.Where(_ => _.ListId == listId).ToListAsync();
        }

        /// <summary>
        /// Creates the item asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        public async Task CreateItemAsync(ToDoItem item)
        {
            this.logger.LogDebug("Creating item {@item}", item);
            await this.entities.AddAsync(item);

            await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the item asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        public async Task UpdateItemAsync(ToDoItem item)
        {
            this.logger.LogDebug("Updating item {@item}", item);
            this.entities.Update(item);

            await this.context.SaveChangesAsync();
        }
    }
}