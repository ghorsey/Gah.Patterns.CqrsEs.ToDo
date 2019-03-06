namespace Gah.Blocks.CqrsEs.Events
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class <c>EventBus</c>.
    /// Implements the <see cref="IEventBus" />
    /// </summary>
    /// <seealso cref="IEventBus" />
    public class EventBus : IEventBus
    {
        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator mediator;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<EventBus> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventBus" /> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="logger">The logger.</param>
        public EventBus(IMediator mediator, ILogger<EventBus> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        /// <summary>
        /// Publishes the asynchronous.
        /// </summary>
        /// <typeparam name="TEvent">The type of the t event.</typeparam>
        /// <param name="events">The events.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        public async Task RaiseAsync<TEvent>(IEnumerable<TEvent> events, CancellationToken cancellationToken = default(CancellationToken))
            where TEvent : IEvent
        {
            var eventList = events.ToList();
            this.logger.LogDebug("Raising {count} events", eventList.Count);
            foreach (var @event in eventList)
            {
                this.logger.LogDebug("Raising event {@event}", @event);
                await this.mediator.Publish(@event, cancellationToken);
            }
        }
    }
}