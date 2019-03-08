namespace Gah.Patterns.ToDo.Command.Domain
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
        public ToDoItem(Guid id, string title)
        {
            this.Id = id;
            this.Title = title;
            this.IsDone = false;
            this.Created = DateTime.UtcNow;
            this.Updated = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; private set;  }

        /// <summary>
        /// Gets a value indicating whether this instance is done.
        /// </summary>
        /// <value><c>true</c> if this instance is done; otherwise, <c>false</c>.</value>
        public bool IsDone { get; private set; }

        /// <summary>
        /// Gets the created.
        /// </summary>
        /// <value>The created.</value>
        public DateTime Created { get; private set; }

        /// <summary>
        /// Gets the updated.
        /// </summary>
        /// <value>The updated.</value>
        public DateTime Updated { get; private set; }
    }
}