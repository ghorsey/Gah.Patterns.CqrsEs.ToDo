namespace Gah.Blocks.CqrsEs.EventStore.InMemory
{
    using System;

    using Gah.Blocks.CqrsEs.Events;

    /// <summary>
    /// Class <c>InMemoryEvent</c>.
    /// </summary>
    public class InMemoryEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="eventNumber">The event number.</param>
        /// <param name="event">The event.</param>
        public InMemoryEvent(Guid id, string stream, long eventNumber, IEvent @event)
        {
            this.Id = id;
            this.Stream = stream;
            this.EventNumber = eventNumber;
            this.Event = @event;
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
        public IEvent Event { get; }
    }
}