namespace Gah.Blocks.CqrsEs
{
    using System.Collections.Generic;

    using Gah.Blocks.CqrsEs.Events;

    /// <summary>
    /// Class <c>AggregateWithEvents</c>.
    /// Implements the <see cref="Gah.Blocks.CqrsEs.IEntityWithEvents{TId}" />
    /// </summary>
    /// <typeparam name="TId">The type of the t identifier.</typeparam>
    /// <seealso cref="Gah.Blocks.CqrsEs.IEntityWithEvents{TId}" />
    public abstract class AggregateWithEvents<TId> : IEntityWithEvents<TId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateWithEvents{TId}"/> class.
        /// </summary>
        protected AggregateWithEvents()
        {
            this.Events = new Queue<IEvent>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public TId Id { get; protected set; }

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <value>The events.</value>
        public Queue<IEvent> Events { get; }

        /// <summary>
        /// Applies the specified event.
        /// </summary>
        /// <param name="event">The event.</param>
        /// <param name="published">if set to <c>true</c> [published].</param>
        public void Apply(IEvent @event, bool published = false)
        {
            this.InvokeEventOptional(@event);
            if (!published)
            {
                this.Events.Enqueue(@event);
            }
        }

        /// <summary>
        /// Applies the specified events.
        /// </summary>
        /// <param name="events">The events.</param>
        /// <param name="published">if set to <c>true</c> [published].</param>
        public void Apply(IEnumerable<IEvent> events, bool published = false)
        {
            foreach (var @event in events)
            {
                this.Apply(@event, published);
            }
        }
    }
}