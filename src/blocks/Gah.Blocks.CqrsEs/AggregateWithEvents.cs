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
        /// <param name="id">The identifier.</param>
        protected AggregateWithEvents(TId id)
        {
            this.Id = id;
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
    }
}