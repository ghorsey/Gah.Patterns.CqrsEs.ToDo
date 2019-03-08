namespace Gah.Blocks.CqrsEs
{
    using System.Collections.Generic;

    using Gah.Blocks.CqrsEs.Events;

    /// <summary>
    /// Interface <c>IEntityWithEvents</c>
    /// Implements the <see cref="Gah.Blocks.CqrsEs.IEntity{TId}" />
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    /// <seealso cref="Gah.Blocks.CqrsEs.IEntity{TId}" />
    public interface IEntityWithEvents<out TId> : IEntity<TId>
    {
        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <value>The events.</value>
        Queue<IEvent> Events { get; }
    }
}