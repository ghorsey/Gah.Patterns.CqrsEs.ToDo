namespace Gah.Blocks.CqrsEs.EventStore
{
    using System.Collections.Generic;

    using Gah.Blocks.CqrsEs.Events;

    /// <summary>
    /// Class <c>ReadStreamResult</c>.
    /// </summary>
    public class ReadStreamResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadStreamResult"/> class.
        /// </summary>
        /// <param name="steam">The steam.</param>
        /// <param name="readDirection">The read direction.</param>
        /// <param name="fromEventNumber">From event number.</param>
        /// <param name="lastEventNumber">The last event number.</param>
        /// <param name="nextEventNumber">The next event number.</param>
        /// <param name="isEndOfStream">if set to <c>true</c> [is end of stream].</param>
        /// <param name="events">The events.</param>
        public ReadStreamResult(
            string steam,
            ReadDirection readDirection,
            long fromEventNumber,
            long lastEventNumber,
            long nextEventNumber,
            bool isEndOfStream,
            IEnumerable<IEvent> events)
        {
            this.Stream = steam;
            this.ReadDirection = readDirection;
            this.FromEventNumber = fromEventNumber;
            this.LastEventNumber = lastEventNumber;
            this.NextEventNumber = nextEventNumber;
            this.IsEndOfStream = isEndOfStream;
            this.Events = events;
        }

        /// <summary>
        /// Gets the stream.
        /// </summary>
        /// <value>The stream.</value>
        public string Stream { get; }

        /// <summary>
        /// Gets the read direction.
        /// </summary>
        /// <value>The read direction.</value>
        public ReadDirection ReadDirection { get; }

        /// <summary>
        /// Gets from event number.
        /// </summary>
        /// <value>From event number.</value>
        public long FromEventNumber { get; }

        /// <summary>
        /// Gets the last event number.
        /// </summary>
        /// <value>The last event number.</value>
        public long LastEventNumber { get; }

        /// <summary>
        /// Gets the next event number.
        /// </summary>
        /// <value>The next event number.</value>
        public long NextEventNumber { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is end of stream.
        /// </summary>
        /// <value><c>true</c> if this instance is end of stream; otherwise, <c>false</c>.</value>
        public bool IsEndOfStream { get; }

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <value>The events.</value>
        public IEnumerable<IEvent> Events { get; }
    }
}