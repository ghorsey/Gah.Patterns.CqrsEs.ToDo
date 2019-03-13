namespace Gah.Patterns.ToDo.Commands.Domain
{
    using System;

    /// <summary>
    /// Class <c>ToDoItem</c>.
    /// </summary>
    public class ToDoItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoItem" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="title">The title.</param>
        /// <param name="isDone">if set to <c>true</c> [is done].</param>
        /// <param name="created">The created.</param>
        /// <param name="updated">The updated.</param>
        public ToDoItem(Guid id, string title, bool isDone, DateTime created, DateTime updated)
        {
            this.Id = id;
            this.Title = title;
            this.IsDone = isDone;
            this.Created = created;
            this.Updated = updated;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is done.
        /// </summary>
        /// <value><c>true</c> if this instance is done; otherwise, <c>false</c>.</value>
        public bool IsDone { get; set; }

        /// <summary>
        /// Gets the created.
        /// </summary>
        /// <value>The created.</value>
        public DateTime Created { get; }

        /// <summary>
        /// Gets or sets the updated.
        /// </summary>
        /// <value>The updated.</value>
        public DateTime Updated { get; set; }
    }
}