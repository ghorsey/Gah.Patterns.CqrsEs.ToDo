namespace Gah.Patterns.ToDo.Query.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Gah.Patterns.ToDo.Query.Domain;

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
        /// Finds the item asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A/an <c>Task&lt;ToDoItem&gt;</c>.</returns>
        Task<ToDoItem> FindItemAsync(Guid id);

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

        /// <summary>
        /// Deletes the item asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        Task DeleteItemAsync(Guid id);

        /// <summary>
        /// Deletes all items for list asynchronous.
        /// </summary>
        /// <param name="listId">The list identifier.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        Task DeleteAllItemsForListAsync(Guid listId);
    }
}