namespace Gah.Patterns.ToDo.Command.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Gah.Blocks.CqrsEs;
    using Gah.Blocks.CqrsEs.Events;
    using Gah.Patterns.ToDo.Command.Domain.Events;
    using Gah.Patterns.ToDo.Command.Domain.Events.Items;
    using Gah.Patterns.ToDo.Command.Domain.Events.Lists;

    /// <summary>
    /// Basic Class
    /// Implements the <see cref="AggregateWithEvents{TId}" /></summary>
    /// <seealso cref="AggregateWithEvents{TId}" />
    public class ToDoList : AggregateWithEvents<Guid>
    {
        /// <summary>
        /// The items
        /// </summary>
        private readonly List<ToDoItem> items;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoList"/> class.
        /// </summary>
        /// <param name="events">The events.</param>
        public ToDoList(IEnumerable<IEvent> events)
            : this()
        {
            this.Apply(events);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoList"/> class.
        /// </summary>
        private ToDoList()
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
        /// <exception cref="InvalidOperationException">Cannot Create an existing item</exception>
        private void When(ListCreatedEvent @event)
        {
            if (!Guid.Empty.Equals(this.Id))
            {
                throw new InvalidOperationException("Cannot Create an existing item");
            }

            this.Title = @event.Title;
            this.Id = @event.Id;
            this.Created = @event.Created;
            this.Updated = @event.Updated;
        }

        /// <summary>
        /// Whens the specified event.
        /// </summary>
        /// <param name="event">The event.</param>
        /// <exception cref="InvalidOperationException">Cannot update a new List, it must be created</exception>
        private void When(ListUpdatedEvent @event)
        {
            if (Guid.Empty.Equals(this.Id))
            {
                throw new InvalidOperationException("Cannot update a new List, it must be created");
            }

            this.Id = @event.Id;
            this.Title = @event.Title;
            this.Updated = @event.Updated;
        }

        /// <summary>
        /// Whens the specified @event.
        /// </summary>
        /// <param name="event">The event.</param>
        private void When(ItemAddedEvent @event)
        {
            var item = new ToDoItem(@event.Id, @event.Title, false, @event.Created, @event.Updated);

            this.items.Add(item);
        }

        /// <summary>
        /// Whens the specified event.
        /// </summary>
        /// <param name="event">The event.</param>
        /// <exception cref="InvalidOperationException">The item {@event.Id}</exception>
        private void When(ItemUpdatedEvent @event)
        {
            var item = this.items.FirstOrDefault(i => i.Id == @event.Id);

            if (item == null)
            {
                throw new InvalidOperationException($"The item {@event.Id} was not found");
            }

            item.Title = @event.Title;
            item.Updated = @event.Updated;
        }

        /// <summary>
        /// Whens the specified event.
        /// </summary>
        /// <param name="event">The event.</param>
        /// <exception cref="InvalidOperationException">The item {@event.Id}</exception>
        private void When(ItemIsDoneUpdated @event)
        {
            var item = this.items.FirstOrDefault(i => i.Id == @event.Id);

            if (item == null)
            {
                throw new InvalidOperationException($"The item {@event.Id} was not found");
            }

            item.IsDone = @event.IsDone;
            item.Updated = @event.Updated;
        }
    }
}
