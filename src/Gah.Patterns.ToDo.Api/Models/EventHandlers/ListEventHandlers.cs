namespace Gah.Patterns.ToDo.Api.Models.EventHandlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.CqrsEs.Events;
    using Gah.Patterns.ToDo.Command.Domain.Events;
    using Gah.Patterns.ToDo.Command.Domain.Events.Lists;
    using Gah.Patterns.ToDo.Query.Repository;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Implements the <see cref="IEventHandler{ListCreatedEvent}" />
    /// Implements the <see cref="IEventHandler{ListUpdated}" />
    /// Implements the <see cref="IEventHandler{ListCountsChanged}" />
    /// </summary>
    /// <seealso cref="IEventHandler{ListCountsChanged}" />
    /// <seealso cref="IEventHandler{ListUpdated}" />
    /// <seealso cref="IEventHandler{ListCreatedEvent}" />
    public class ListEventHandlers : IEventHandler<ListCreatedEvent>, IEventHandler<ListUpdatedEvent>, IEventHandler<ListCountsChangedEvent>
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IToDoListRepository repository;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListEventHandlers"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="logger">The logger.</param>
        public ListEventHandlers(IToDoListRepository repository, ILogger<ListEventHandlers> logger)
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
        public async Task Handle(ListCreatedEvent notification, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("A list was created {@event}", notification);
            var list = new Query.Domain.ToDoList(
                notification.Id,
                notification.Title,
                0,
                0,
                0,
                notification.Updated,
                notification.Created);

            await this.repository.CreateListAsync(list);
        }

        /// <summary>
        /// Handles the specified notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        public async Task Handle(ListUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var list = await this.repository.FindListAsync(notification.Id);

            var updated = new Query.Domain.ToDoList(
                list.Id,
                notification.Title,
                list.TotalItems,
                list.TotalCompleted,
                list.TotalPending,
                notification.Updated,
                list.Created);

            await this.repository.UpdateListAsync(updated);
        }

        /// <summary>
        /// Handles the specified notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        public async Task Handle(ListCountsChangedEvent notification, CancellationToken cancellationToken)
        {
            var list = await this.repository.FindListAsync(notification.Id);

            var updated = new Query.Domain.ToDoList(
                list.Id,
                list.Title,
                notification.TotalItems,
                notification.CompletedItems,
                notification.PendingItems,
                list.Updated,
                list.Created);

            await this.repository.UpdateListAsync(updated);
        }
    }
}
