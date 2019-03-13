namespace Gah.Blocks.CqrsEs.EventStore.Sql.Entities
{
    using System;

    using Gah.Blocks.CqrsEs.Events;

    /// <summary>
    /// Class <c>Event</c>.
    /// Implements the <see cref="Gah.Blocks.CqrsEs.Events.IEvent" />
    /// </summary>
    /// <seealso cref="Gah.Blocks.CqrsEs.Events.IEvent" />
    public class EventSource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventSource"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="eventNumber">The event number.</param>
        /// <param name="event">The event.</param>
        /// <param name="eventType">Type of the event.</param>
        public EventSource(Guid id, string stream, long eventNumber, string @event, string eventType)
        {
            this.Id = id;
            this.Stream = stream;
            this.EventNumber = eventNumber;
            this.Event = @event;
            this.EventType = eventType;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; }

        /// <summary>
        /// Gets the stream.
        /// </summary>
        /// <value>The stream.</value>
        public string Stream { get; }

        /// <summary>
        /// Gets the event number.
        /// </summary>
        /// <value>The event number.</value>
        public long EventNumber { get; }

        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <value>The event.</value>
        public string Event { get; }

        /// <summary>
        /// Gets the type of the event.
        /// </summary>
        /// <value>The type of the event.</value>
        public string EventType { get; }
    }
}
