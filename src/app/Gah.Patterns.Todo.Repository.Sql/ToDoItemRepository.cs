namespace Gah.Patterns.Todo.Repository.Sql
{
    using System;
    using System.Collections.Generic;
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
        private readonly ApplicationDbContext context;

        private readonly DbSet<ToDoItem> entities;

        private readonly ILogger logger; 
        
        /// <summary>
        /// Finds the items asynchronous.
        /// </summary>
        /// <param name="listId">The list identifier.</param>
        /// <returns>A/an <c>Task&lt;List&lt;ToDoItem&gt;&gt;</c>.</returns>
        public Task<List<ToDoItem>> FindItemsAsync(Guid listId)
        {
            return 
        }

        /// <summary>
        /// Creates the item asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        public Task CreateItemAsync(ToDoItem item)
        {
        }

        /// <summary>
        /// Updates the item asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        public Task UpdateItemAsync(ToDoItem item)
        {
        }
    }
}