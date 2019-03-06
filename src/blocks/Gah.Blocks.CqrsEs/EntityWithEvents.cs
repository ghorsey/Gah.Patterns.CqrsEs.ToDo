namespace Gah.Blocks.CqrsEs
{
    using System.Collections.Generic;

    using Gah.Blocks.CqrsEs.Events;

    /// <summary>
    /// Class <c>EntityWithEvents</c>.
    /// Implements the <see cref="Gah.Blocks.CqrsEs.IEntityWithEvents{TId, TEvent}" />
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    /// <typeparam name="TEvent">The type of the t event.</typeparam>
    /// <seealso cref="Gah.Blocks.CqrsEs.IEntityWithEvents{TId, TEvent}" />
    /// <seealso cref="Gah.Blocks.CqrsEs.IEntity{TId}" />
    public abstract class EntityWithEvents<TId, TEvent> : IEntityWithEvents<TId, TEvent>
        where TEvent : IEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityWithEvents{TId, TEvent}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        protected EntityWithEvents(TId id)
        {
            this.Id = id;
            this.Events = new Queue<TEvent>();
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public TId Id { get; }

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <value>The events.</value>
        public Queue<TEvent> Events { get; }
    }
}