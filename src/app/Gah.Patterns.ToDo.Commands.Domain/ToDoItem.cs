namespace Gah.Patterns.ToDo.Commands.Domain
{
    using System;

    using Gah.Patterns.ToDo.Events.Items;

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
        /// Initializes a new instance of the <see cref="ToDoItem"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="title">The title.</param>
        public ToDoItem(Guid id, string title)
            : this(id, title, false, DateTime.UtcNow, DateTime.UtcNow)
        {
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is done.
        /// </summary>
        /// <value><c>true</c> if this instance is done; otherwise, <c>false</c>.</value>
        public bool IsDone { get; private set;  }

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

        /// <summary>
        /// Gets or sets the list.
        /// </summary>
        /// <value>The list.</value>
        internal ToDoList List { get; set; }

        /// <summary>
        /// Sets the title.
        /// </summary>
        /// <param name="title">The title.</param>
        public void SetTitle(string title)
        {
            this.Title = title;
            this.Updated = DateTime.UtcNow;

            this.List.Events.Enqueue(new ItemUpdatedEvent(this.Id, this.List.Id, this.Title, this.Updated));
        }

        /// <summary>
        /// Sets the is done.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public void SetIsDone(bool value)
        {
            this.IsDone = value;
            this.Updated = DateTime.UtcNow;

            this.List.Events.Enqueue(
                new ItemIsDoneUpdatedEvent(
                    this.Id,
                    this.List.Id,
                    this.IsDone,
                    this.Updated));

            this.List.Events.Enqueue(this.List.CreateListCountsChangedEvent());
        }

        /// <summary>
        /// Sets the list.
        /// </summary>
        /// <param name="list">The list.</param>
        internal void SetList(ToDoList list)
        {
            this.List = list;
        }
    }
}