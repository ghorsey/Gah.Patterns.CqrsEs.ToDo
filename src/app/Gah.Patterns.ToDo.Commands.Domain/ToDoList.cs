namespace Gah.Patterns.ToDo.Commands.Domain
{
    // ReSharper disable UnusedMember.Local
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Gah.Blocks.CqrsEs;
    using Gah.Blocks.CqrsEs.Events;
    using Gah.Patterns.ToDo.Events.Items;
    using Gah.Patterns.ToDo.Events.Lists;

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
            this.Apply(events, true);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoList"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="title">The title.</param>
        public ToDoList(Guid id, string title)
            : this()
        {
            this.Id = id;
            this.Title = title;
            this.Created = DateTime.UtcNow;
            this.Updated = DateTime.UtcNow;

            this.Events.Enqueue(
                new ListCreatedEvent(
                    this.Id,
                    this.Title,
                    this.Updated,
                    this.Created));
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ToDoList"/> class from being created.
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
            item.SetList(this);

            this.Events.Enqueue(new ItemAddedEvent(item.Id, this.Id, item.Title, item.Created, item.Updated));
            this.Events.Enqueue(this.CreateListCountsChangedEvent());
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Remove(ToDoItem item)
        {
            this.items.Remove(item);

            this.Events.Enqueue(new ItemDeletedEvent(item.Id, this.Id));
            this.Events.Enqueue(this.CreateListCountsChangedEvent());
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        public void Delete()
        {
            var deletedEvent = new ListDeletedEvent(this.Id);

            this.Events.Enqueue(deletedEvent);
        }

        /// <summary>
        /// Updates the specified title.
        /// </summary>
        /// <param name="title">The title.</param>
        public void Update(string title)
        {
            this.Title = title;
            this.Updated = DateTime.UtcNow;

            this.Events.Enqueue(new ListUpdatedEvent(this.Id, this.Title, this.Updated));
        }

        /// <summary>
        /// Creates the list counts changed event.
        /// </summary>
        /// <returns>A/an <c>ListCountsChangedEvent</c>.</returns>
        internal ListCountsChangedEvent CreateListCountsChangedEvent()
        {
            return new ListCountsChangedEvent(
                this.Id,
                this.Items.Count,
                this.Items.Count(i => !i.IsDone),
                this.Items.Count(i => i.IsDone));
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
            item.SetList(this);

            this.items.Add(item);
        }

        /// <summary>
        /// Whens the specified event.
        /// </summary>
        /// <param name="event">The event.</param>
        /// <exception cref="InvalidOperationException">The item {@event.Id}</exception>
        private void When(ItemUpdatedEvent @event)
        {
            var item = this.items.First(i => i.Id == @event.Id);

            item.SetTitle(@event.Title);
            item.Updated = @event.Updated;
        }

        /// <summary>
        /// Whens the specified event.
        /// </summary>
        /// <param name="event">The event.</param>
        /// <exception cref="InvalidOperationException">The item {@event.Id}</exception>
        private void When(ItemIsDoneUpdatedEvent @event)
        {
            var item = this.items.First(i => i.Id == @event.Id);
            item.SetIsDone(@event.IsDone);
            item.Updated = @event.Updated;
        }

        /// <summary>
        /// Whens the specified event.
        /// </summary>
        /// <param name="event">The event.</param>
        private void When(ItemDeletedEvent @event)
        {
            var item = this.items.First(i => i.Id == @event.Id);

            this.items.Remove(item);
        }
    }
}
