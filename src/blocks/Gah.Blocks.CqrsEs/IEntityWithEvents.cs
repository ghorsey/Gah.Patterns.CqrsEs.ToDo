namespace Gah.Blocks.CqrsEs
{
    using System.Collections.Generic;

    using Gah.Blocks.CqrsEs.Events;

    /// <summary>
    /// Interface <c>IEntityWithEvents</c>
    /// Implements the <see cref="Gah.Blocks.CqrsEs.IEntity{TId}" />
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    /// <seealso cref="Gah.Blocks.CqrsEs.IEntity{TId}" />
    public interface IEntityWithEvents<out TId, TEvent> : IEntity<TId>
        where TEvent : IEvent
    {
        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <value>The events.</value>
        Queue<TEvent> Events { get; }
    }
}