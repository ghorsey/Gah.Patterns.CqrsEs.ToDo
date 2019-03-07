namespace Gah.Patterns.Todo.Domain
{
    using System;

    /// <summary>
    /// Class <c>ToDoItem</c>.
    /// </summary>
    public class ToDoItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoItem"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="listId">The list identifier.</param>
        /// <param name="title">The title.</param>
        /// <param name="isDone">if set to <c>true</c> [is done].</param>
        /// <param name="updated">The updated.</param>
        /// <param name="created">The created.</param>
        public ToDoItem(Guid id, Guid listId, string title, bool isDone, DateTime updated, DateTime created)
        {
            this.Id = id;
            this.ListId = listId;
            this.Title = title;
            this.IsDone = isDone;
            this.Updated = updated;
            this.Created = created;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; }

        /// <summary>
        /// Gets the list identifier.
        /// </summary>
        /// <value>The list identifier.</value>
        public Guid ListId { get; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is done.
        /// </summary>
        /// <value><c>true</c> if this instance is done; otherwise, <c>false</c>.</value>
        public bool IsDone { get; }

        /// <summary>
        /// Gets the created.
        /// </summary>
        /// <value>The created.</value>
        public DateTime Created { get; }

        /// <summary>
        /// Gets the updated.
        /// </summary>
        /// <value>The updated.</value>
        public DateTime Updated { get; }
    }
}
