namespace Gah.Patterns.ToDo.Command.Domain.Commands.Lists
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.CqrsEs.Commands;
    using Gah.Blocks.CqrsEs.Events;
    using Gah.Blocks.CqrsEs.EventStore;
    using Gah.Patterns.ToDo.Command.Domain.Events;
    using MediatR;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class <c>ListCommandHandler</c>.
    /// Implements the <see cref="Gah.Blocks.CqrsEs.Commands.ICommandHandler{CreateListCommand}" />
    /// </summary>
    /// <seealso cref="Gah.Blocks.CqrsEs.Commands.ICommandHandler{CreateListCommand}" />
    /// <inheritdoc />
    public class ListCommandHandler
        : ICommandHandler<CreateListCommand>
    {
        /// <summary>
        /// The event store
        /// </summary>
        private readonly IEventStore eventStore;

        /// <summary>
        /// The event bus
        /// </summary>
        private readonly IEventBus eventBus;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListCommandHandler" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="eventStore">The event store.</param>
        /// <param name="logger">The logger.</param>
        public ListCommandHandler(IEventBus eventBus, IEventStore eventStore, ILogger<ListCommandHandler> logger)
        {
            this.eventBus = eventBus;
            this.logger = logger;
            this.eventStore = eventStore;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task&lt;Unit&gt;</c>.</returns>
        public async Task<Unit> Handle(CreateListCommand request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Got command {@event}", request);

            var listCreated = new ListCreatedEvent(
                request.Id,
                request.Title,
                DateTime.UtcNow,
                DateTime.UtcNow);

            var list = new ToDoList();
            var eventResult = await this.eventStore.ReadAllForwardAsync(listCreated.Id.ToString());
            list.Apply(eventResult.Events);
            list.Apply(listCreated);

            await this.eventStore.AppendToStreamAsync(list.Id.ToString(), 1, listCreated);

            await this.eventBus.PublishAsync(
                new[]
                {
                    listCreated
                },
                cancellationToken);
            return Unit.Value;
        }
    }
}