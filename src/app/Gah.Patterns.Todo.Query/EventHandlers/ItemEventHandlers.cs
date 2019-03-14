namespace Gah.Patterns.ToDo.Query.EventHandlers
{
    using System.Threading;
    using System.Threading.Tasks;

    using Gah.Blocks.CqrsEs.Events;
    using Gah.Patterns.ToDo.Events.Items;
    using Gah.Patterns.ToDo.Events.Lists;
    using Gah.Patterns.ToDo.Query.Domain;
    using Gah.Patterns.ToDo.Query.Repository;

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The item event handler.
    /// Implements the <see cref="IEventHandler{ItemAddedEvent}" />
    /// Implements the <see cref="IEventHandler{ItemUpdatedEvent}" />
    /// Implements the <see cref="IEventHandler{ItemIsDoneUpdated}" />
    /// Implements the <see cref="IEventHandler{ItemDeletedEvent}" />
    /// Implements the <see cref="IEventHandler{ListDeletedEvent}" />
    /// </summary>
    /// <seealso cref="IEventHandler{ItemUpdatedEvent}" />
    /// <seealso cref="IEventHandler{ItemIsDoneUpdated}" />
    /// <seealso cref="IEventHandler{ItemAddedEvent}" />
    /// <seealso cref="IEventHandler{ItemDeletedEvent}" />
    /// <seealso cref="IEventHandler{ListDeletedEvent}" />
    public class ItemEventHandlers : IEventHandler<ItemAddedEvent>,
                                    IEventHandler<ItemUpdatedEvent>,
                                    IEventHandler<ItemIsDoneUpdatedEvent>,
                                    IEventHandler<ItemDeletedEvent>,
                                    IEventHandler<ListDeletedEvent>
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
        /// Initializes a new instance of the <see cref="ItemEventHandlers"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="logger">The logger.</param>
        public ItemEventHandlers(IToDoItemRepository repository, ILogger<ItemEventHandlers> logger)
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

            var item = new ToDoItem(
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

            var updated = new ToDoItem(
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
        public async Task Handle(ItemIsDoneUpdatedEvent notification, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Updating the item is done status {@event}", notification);
            var item = await this.repository.FindItemAsync(notification.Id);

            var updated = new ToDoItem(
                item.Id,
                item.ListId,
                item.Title,
                notification.IsDone,
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
        public async Task Handle(ItemDeletedEvent notification, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Deleting item {id}", notification.Id);

            await this.repository.DeleteItemAsync(notification.Id);
        }

        /// <summary>
        /// Handles the specified notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        public async Task Handle(ListDeletedEvent notification, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Deleting all items for list {listId}", notification.Id);

            await this.repository.DeleteAllItemsForListAsync(notification.Id);
        }
    }
}