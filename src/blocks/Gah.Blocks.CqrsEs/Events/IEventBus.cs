namespace Gah.Blocks.CqrsEs.Events
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface <c>IEventBus</c>
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Publishes the asynchronous.
        /// </summary>
        /// <typeparam name="TEvent">The type of the t event.</typeparam>
        /// <param name="events">The events.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        Task PublishAsync<TEvent>(IEnumerable<TEvent> events, CancellationToken cancellationToken = default(CancellationToken))
            where TEvent : IEvent;
    }
}