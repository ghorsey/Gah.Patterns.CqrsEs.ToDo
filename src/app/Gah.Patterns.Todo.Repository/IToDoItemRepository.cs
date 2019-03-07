namespace Gah.Patterns.ToDo.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Gah.Patterns.ToDo.Domain;

    /// <summary>
    /// Interface <c>IToDoItemRepository</c>
    /// </summary>
    public interface IToDoItemRepository
    {
        /// <summary>
        /// Finds the items asynchronous.
        /// </summary>
        /// <param name="listId">The list identifier.</param>
        /// <returns>A/an <c>Task&lt;List&lt;ToDoItem&gt;&gt;</c>.</returns>
        Task<List<ToDoItem>> FindItemsAsync(Guid listId);

        /// <summary>
        /// Creates the item asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        Task CreateItemAsync(ToDoItem item);

        /// <summary>
        /// Updates the item asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        Task UpdateItemAsync(ToDoItem item);
    }
}