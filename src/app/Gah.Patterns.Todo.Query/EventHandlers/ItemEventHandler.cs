namespace Gah.Patterns.ToDo.Query.EventHandlers
{
    using System.Threading;
    using System.Threading.Tasks;

    using Gah.Blocks.CqrsEs.Events;
    using Gah.Patterns.ToDo.Events.Items;
    using Gah.Patterns.ToDo.Query.Repository;

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The item event handler.
    /// Implements the <see cref="IEventHandler{ItemAddedEvent}" />
    /// Implements the <see cref="IEventHandler{ItemUpdatedEvent}" />
    /// Implements the <see cref="IEventHandler{ItemIsDoneUpdated}" />
    /// </summary>
    /// <seealso cref="IEventHandler{ItemUpdatedEvent}" />
    /// <seealso cref="IEventHandler{ItemIsDoneUpdated}" />
    /// <seealso cref="IEventHandler{ItemAddedEvent}" />
    public class ItemEventHandler : IEventHandler<ItemAddedEvent>,
                                    IEventHandler<ItemUpdatedEvent>,
                                    IEventHandler<ItemIsDoneUpdated>
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IToDoItemRepository repository;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemEventHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="logger">The logger.</param>
        public ItemEventHandler(IToDoItemRepository repository, ILogger<ItemEventHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        /// <summary>
        /// Handles the specified notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        public async Task Handle(ItemAddedEvent notification, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Got the item added event {@event}", notification);

            var item = new Query.Domain.ToDoItem(
                notification.Id,
                notification.ListId,
                notification.Title,
                false,
                notification.Updated,
                notification.Created);

            await this.repository.CreateItemAsync(item);
        }

        /// <summary>
        /// Handles the specified notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        public async Task Handle(ItemUpdatedEvent notification, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Got the update item event {@event}", notification);

            var item = await this.repository.FindItemAsync(notification.Id);

            var updated = new Query.Domain.ToDoItem(
                item.Id,
                item.ListId,
                notification.Title,
                item.IsDone,
                notification.Updated,
                item.Created);

            await this.repository.UpdateItemAsync(updated);
        }

        /// <summary>
        /// Handles the specified notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        public async Task Handle(ItemIsDoneUpdated notification, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Updating the item is done status {@event}", notification);
            var item = await this.repository.FindItemAsync(notification.Id);

            var updated = new Query.Domain.ToDoItem(
                item.Id,
                item.ListId,
                item.Title,
                notification.IsDone,
                notification.Updated,
                item.Created);

            await this.repository.UpdateItemAsync(updated);
        }
    }
}