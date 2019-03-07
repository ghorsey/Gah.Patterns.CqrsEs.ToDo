namespace Gah.Patterns.Todo.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Gah.Patterns.Todo.Domain;

    /// <summary>
    /// Basic Class
    /// </summary>
    public interface ITodoListRepository
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
    }
}
