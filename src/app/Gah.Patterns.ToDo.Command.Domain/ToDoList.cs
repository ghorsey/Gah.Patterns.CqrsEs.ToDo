namespace Gah.Patterns.ToDo.Command.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Gah.Blocks.CqrsEs;
    using Gah.Blocks.CqrsEs.Events;
    using Gah.Patterns.ToDo.Command.Domain.Events;

    /// <summary>
    /// Basic Class
    /// Implements the <see cref="AggregateWithEvents{TId}" /></summary>
    /// <seealso cref="AggregateWithEvents{TId}" />
    public class ToDoList : AggregateWithEvents<Guid>
    {
        private readonly List<ToDoItem> items;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoList"/> class.
        /// </summary>
        public ToDoList()
        {
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
        public ReadOnlyCollection<ToDoItem> Items => this.items.AsReadOnly();

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

        /// <summary>
        /// Whens the specified @event.
        /// </summary>
        /// <param name="event">The event.</param>
        private void When(ListCreatedEvent @event)
        {
            this.Title = @event.Title;
            this.Id = @event.Id;
            this.Created = @event.Created;
            this.Updated = @event.Updated;
        }
    }
}
