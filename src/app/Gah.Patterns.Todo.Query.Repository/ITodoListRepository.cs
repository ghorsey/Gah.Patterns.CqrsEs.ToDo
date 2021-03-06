namespace Gah.Patterns.ToDo.Query.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Gah.Patterns.ToDo.Query.Domain;

    /// <summary>
    /// Basic Class
    /// </summary>
    public interface IToDoListRepository
    {
        /// <summary>
        /// Finds the items.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns>A/an <c>Task&lt;List&lt;System.Int32&gt;&gt;</c>.</returns>
        Task<List<ToDoList>> FindListsAsync(string title = "");

        /// <summary>
        /// Finds the list.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A/an <c>Task&lt;ToDoList&gt;</c>.</returns>
        Task<ToDoList> FindListAsync(Guid id);

        /// <summary>
        /// Creates the list.
        /// </summary>
        /// <param name="toDoList">To do list.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        Task CreateListAsync(ToDoList toDoList);

        /// <summary>
        /// Updates the list.
        /// </summary>
        /// <param name="toDoList">To do list.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        Task UpdateListAsync(ToDoList toDoList);

        /// <summary>
        /// Deletes the list asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        Task DeleteListAsync(Guid id);
    }
}
