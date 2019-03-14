namespace Gah.Patterns.ToDo.Query.Repository.Sql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Gah.Patterns.ToDo.Query.Domain;
    using Gah.Patterns.ToDo.Query.Repository;
    using Gah.Patterns.ToDo.Query.Repository.Sql.Data;

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
        public ToDoItemRepository(ApplicationDbContext context, ILogger<ToDoItemRepository> logger)
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
            return this.entities.Where(i => i.ListId == listId).OrderBy(i => i.Updated).ToListAsync();
        }

        /// <summary>
        /// Finds the item asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A/an <c>Task&lt;ToDoItem&gt;</c>.</returns>
        public Task<ToDoItem> FindItemAsync(Guid id)
        {
            this.logger.LogDebug("Finding the list item {id}", id);
            return this.entities.FirstOrDefaultAsync(i => i.Id.Equals(id));
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
            var toDelete = await this.entities.FirstOrDefaultAsync(i => i.Id == item.Id);
            this.entities.Remove(toDelete);
            await this.entities.AddAsync(item);

            await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// delete item as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        public async Task DeleteItemAsync(Guid id)
        {
            this.logger.LogDebug("Deleting item {item}", id);

            var toDelete = await this.entities.FirstOrDefaultAsync(i => i.Id == id);

            this.entities.Remove(toDelete);

            await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// delete all items for list as an asynchronous operation.
        /// </summary>
        /// <param name="listId">The list identifier.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        public async Task DeleteAllItemsForListAsync(Guid listId)
        {
            this.logger.LogDebug("Deleting all items for list {listId}", listId);

            var toDelete = await this.entities.Where(i => i.ListId == listId).ToListAsync();

            this.entities.RemoveRange(toDelete);

            await this.context.SaveChangesAsync();
        }
    }
}