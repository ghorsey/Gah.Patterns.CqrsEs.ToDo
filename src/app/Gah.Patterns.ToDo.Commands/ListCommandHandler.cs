namespace Gah.Patterns.ToDo.Commands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Gah.Blocks.CqrsEs.Commands;
    using Gah.Blocks.CqrsEs.Events;
    using Gah.Blocks.CqrsEs.EventStore;
    using Gah.Patterns.ToDo.Commands.Items;
    using Gah.Patterns.ToDo.Commands.Lists;
    using Gah.Patterns.ToDo.Domain;

    using MediatR;

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class <c>ListCommandHandler</c>.
    /// Implements the <see cref="ICommandHandler{CreateListCommand}" />
    /// Implements the <see cref="ICommandHandler{UpdateListCommand}" />
    /// Implements the <see cref="ICommandHandler{CreateItemCommand}" />
    /// Implements the <see cref="ICommandHandler{UpdateItemIsDoneCommand}" />
    /// Implements the <see cref="ICommandHandler{DeleteItemCommand}" />
    /// Implements the <see cref="ICommandHandler{DeleteListCommand}" />
    /// </summary>
    /// <seealso cref="ICommandHandler{UpdateItemCommand}" />
    /// <seealso cref="ICommandHandler{CreateItemCommand}" />
    /// <seealso cref="ICommandHandler{CreateListCommand}" />
    /// <seealso cref="ICommandHandler{UpdateListCommand}" />
    /// <seealso cref="ICommandHandler{UpdateItemIsDoneCommand}" />
    /// <seealso cref="ICommandHandler{DeleteItemCommand}" />
    /// <seealso cref="ICommandHandler{DeleteListCommand}" />
    /// <inheritdoc cref="ICommandHandler{TCommand}" />
    public class ListCommandHandler
        : ICommandHandler<CreateListCommand>,
          ICommandHandler<UpdateListCommand>,
          ICommandHandler<CreateItemCommand>,
          ICommandHandler<UpdateItemCommand>,
          ICommandHandler<UpdateItemIsDoneCommand>,
          ICommandHandler<DeleteItemCommand>,
          ICommandHandler<DeleteListCommand>
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
        public Task<Unit> Handle(CreateListCommand request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Got command {@event}", request);
            var toDoList = new ToDoList(request.Id, request.Title);

            return this.PublishEvents(toDoList, 1, cancellationToken);
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

            var eventResult = await this.eventStore.ReadAllForwardAsync(request.Id.ToString())
                                  .ConfigureAwait(false);

            var list = new ToDoList(eventResult.Events);

            list.Update(request.Title);

            return await this.PublishEvents(list, eventResult.NextEventNumber, cancellationToken);
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

            var item = new ToDoItem(request.Id, request.Title);
            var list = new ToDoList(eventResults.Events);

            list.Add(item);
            return await this.PublishEvents(list, eventResults.NextEventNumber, cancellationToken);
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

            var item = list.Items.First(i => i.Id == request.Id);

            item.SetTitle(request.Title);

            return await this.PublishEvents(list, events.NextEventNumber, cancellationToken);
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
            var item = list.Items.First(i => i.Id == request.Id);
            item.SetIsDone(request.IsDone);

            return await this.PublishEvents(list, events.NextEventNumber, cancellationToken);
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task&lt;Unit&gt;</c>.</returns>
        public async Task<Unit> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Attempting to delete an item with the command {@item}", request);

            var eventStream = await this.eventStore.ReadAllForwardAsync(request.ListId.ToString());

            var list = new ToDoList(eventStream.Events);
            var item = list.Items.First(i => i.Id == request.Id);

            list.Remove(item);

            return await this.PublishEvents(list, eventStream.NextEventNumber, cancellationToken);
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task&lt;Unit&gt;</c>.</returns>
        public async Task<Unit> Handle(DeleteListCommand request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug(
                "Attempting to delete a list with the command {@command}",
                request);

            var eventStream = await this.eventStore.ReadAllForwardAsync(request.Id.ToString());

            var list = new ToDoList(eventStream.Events);

            list.Delete();

            return await this.PublishEvents(list, eventStream.NextEventNumber, cancellationToken);
        }

        /// <summary>
        /// Publishes the events.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="expectedVersion">The expected version.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task&lt;Unit&gt;.</c></returns>
        private async Task<Unit> PublishEvents(ToDoList list, long expectedVersion, CancellationToken cancellationToken)
        {
            this.logger.LogDebug(
                "About to save starting version {expectedVersion} of {count} event/s",
                expectedVersion,
                list.Events);

            await this.eventStore.AppendToStreamAsync(
                list.Id.ToString(),
                expectedVersion,
                list.Events);

            await this.eventBus.PublishAsync(list.Events, cancellationToken);

            return Unit.Value;
        }
    }
}