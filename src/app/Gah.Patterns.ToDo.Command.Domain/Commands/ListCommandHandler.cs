namespace Gah.Patterns.ToDo.Command.Domain.Commands
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Gah.Blocks.CqrsEs.Commands;
    using Gah.Blocks.CqrsEs.Events;
    using Gah.Blocks.CqrsEs.EventStore;
    using Gah.Patterns.ToDo.Command.Domain.Commands.Items;
    using Gah.Patterns.ToDo.Command.Domain.Commands.Lists;
    using Gah.Patterns.ToDo.Command.Domain.Events.Items;
    using Gah.Patterns.ToDo.Command.Domain.Events.Lists;

    using MediatR;

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class <c>ListCommandHandler</c>.
    /// Implements the <see cref="ICommandHandler{CreateListCommand}" />
    /// Implements the <see cref="ICommandHandler{UpdateListCommand}" />
    /// Implements the <see cref="ICommandHandler{CreateItemCommand}" />
    /// Implements the <see cref="ICommandHandler{UpdateItemIsDoneCommand}" />
    /// </summary>
    /// <seealso cref="ICommandHandler{UpdateItemCommand}" />
    /// <seealso cref="ICommandHandler{CreateItemCommand}" />
    /// <seealso cref="ICommandHandler{CreateListCommand}" />
    /// <seealso cref="ICommandHandler{UpdateListCommand}" />
    /// <seealso cref="ICommandHandler{UpdateItemIsDoneCommand}" />
    /// <inheritdoc cref="ICommandHandler{TCommand}" />
    public class ListCommandHandler
        : ICommandHandler<CreateListCommand>,
          ICommandHandler<UpdateListCommand>,
          ICommandHandler<CreateItemCommand>,
          ICommandHandler<UpdateItemCommand>,
          ICommandHandler<UpdateItemIsDoneCommand>
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

            var eventResult = await this.eventStore.ReadAllForwardAsync(listCreated.Id.ToString());
            var list = new ToDoList(eventResult.Events);

            list.Apply(listCreated);

            this.logger.LogDebug("About to save version 1 of event {@event}", listCreated);
            await this.eventStore.AppendToStreamAsync(list.Id.ToString(), 1, listCreated)
                .ConfigureAwait(false);

            await this.eventBus.PublishAsync(new[] { listCreated }, cancellationToken)
                .ConfigureAwait(false);

            return Unit.Value;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task&lt;Unit&gt;</c>.</returns>
        public async Task<Unit> Handle(
            UpdateListCommand request,
            CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Got command {@event}", request);

            var listUpdated = new ListUpdatedEvent(request.Id, request.Title, DateTime.UtcNow);

            var eventResult = await this.eventStore.ReadAllForwardAsync(request.Id.ToString())
                                  .ConfigureAwait(false);

            var list = new ToDoList(eventResult.Events);

            list.Apply(listUpdated);

            this.logger.LogDebug(
                "Attempting to save the version {version} for event {@event}",
                eventResult.NextEventNumber,
                listUpdated);

            await this.eventStore.AppendToStreamAsync(
                    list.Id.ToString(),
                    eventResult.NextEventNumber,
                    listUpdated)
                .ConfigureAwait(false);

            await this.eventBus.PublishAsync(new[] { listUpdated }, cancellationToken)
                .ConfigureAwait(false);

            return Unit.Value;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task&lt;Unit&gt;</c>.</returns>
        public async Task<Unit> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug(
                "Attempting to add item {item} to the list id {id}",
                request.Title,
                request.Id);

            var eventResults = await this.eventStore.ReadAllForwardAsync(request.ListId.ToString());

            var list = new ToDoList(eventResults.Events);

            var addItemEvent = new ItemAddedEvent(
                request.Id,
                request.ListId,
                request.Title,
                DateTime.UtcNow,
                DateTime.UtcNow);

            list.Apply(addItemEvent);

            var countsChanged = this.CreateListCountsChangedEvent(list);

            this.logger.LogDebug(
                "About to save item added event {@event} for list {id}",
                addItemEvent,
                list.Id);

            this.logger.LogDebug(
                "About to save event counts changed {@countsChanged} for list {id}",
                countsChanged,
                list.Id);

            await this.eventStore.AppendToStreamAsync(
                list.Id.ToString(),
                eventResults.NextEventNumber,
                addItemEvent,
                countsChanged);

            await this.eventBus.PublishAsync(
                new IEvent[] { addItemEvent, countsChanged },
                cancellationToken);

            return Unit.Value;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task&lt;Unit&gt;</c>.</returns>
        public async Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Attempting to update th item with {@command}", request);

            var events = await this.eventStore.ReadAllForwardAsync(request.ListId.ToString());

            var list = new ToDoList(events.Events);

            var itemUpdated = new ItemUpdatedEvent(
                request.Id,
                request.ListId,
                request.Title,
                DateTime.UtcNow);

            list.Apply(itemUpdated);

            await this.eventStore.AppendToStreamAsync(
                request.ListId.ToString(),
                events.NextEventNumber,
                itemUpdated);

            await this.eventBus.PublishAsync(new[] { itemUpdated }, cancellationToken);

            return Unit.Value;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task&lt;Unit&gt;</c>.</returns>
        public async Task<Unit> Handle(UpdateItemIsDoneCommand request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Attempting to update the is done status with {@event}", request);

            var events = await this.eventStore.ReadAllForwardAsync(request.ListId.ToString());

            var list = new ToDoList(events.Events);

            var isDoneUpdated = new ItemIsDoneUpdated(
                request.Id,
                request.ListId,
                request.IsDone,
                DateTime.UtcNow);

            list.Apply(isDoneUpdated);

            var countsChangedEvent = this.CreateListCountsChangedEvent(list);

            this.logger.LogDebug(
                "About to save is done event {@event} for list {id}",
                isDoneUpdated,
                list.Id);

            this.logger.LogDebug(
                "About to save event counts changed {@countsChanged} for list {id}",
                countsChangedEvent,
                list.Id);

            await this.eventStore.AppendToStreamAsync(
                request.ListId.ToString(),
                events.NextEventNumber,
                isDoneUpdated,
                countsChangedEvent);

            await this.eventBus.PublishAsync(
                new IEvent[] { isDoneUpdated, countsChangedEvent },
                cancellationToken);

            return Unit.Value;
        }

        /// <summary>
        /// Creates the list counts changed event.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>A/an <c>ListCountsChangedEvent</c>.</returns>
        private ListCountsChangedEvent CreateListCountsChangedEvent(ToDoList list)
        {
            return new ListCountsChangedEvent(
                list.Id,
                list.Items.Count,
                list.Items.Count(i => !i.IsDone),
                list.Items.Count(i => i.IsDone));
        }
    }
}