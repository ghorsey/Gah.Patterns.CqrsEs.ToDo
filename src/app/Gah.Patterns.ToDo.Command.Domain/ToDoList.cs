namespace Gah.Patterns.ToDo.Command.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Gah.Blocks.CqrsEs;

    /// <summary>
    /// Basic Class
    /// </summary>
    public class ToDoList : AggregateWithEvents<Guid>
    {
        private readonly List<ToDoItem> items;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoList"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="title">The title.</param>
        public ToDoList(Guid id, string title)
            : base(id)
        {
            this.Title = title;
            this.Created = DateTime.UtcNow;
            this.Updated = DateTime.UtcNow;
            this.items = new List<ToDoItem>();
        }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; private set; }

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

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>The items.</value>
        public ReadOnlyCollection<ToDoItem> Items
        {
            get { return this.items.AsReadOnly(); }
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(ToDoItem item)
        {
            this.items.Add(item);
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Remove(ToDoItem item)
        {
            this.items.Remove(item);
        }
    }
}
