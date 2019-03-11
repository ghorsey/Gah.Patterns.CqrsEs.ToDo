namespace Gah.Patterns.ToDo.Api.Models.EventHandlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.CqrsEs.Events;
    using Gah.Patterns.ToDo.Command.Domain.Events;
    using Gah.Patterns.ToDo.Query.Repository;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Implements the <see cref="IEventHandler{ListCreatedEvent}" />
    /// </summary>
    /// <seealso cref="IEventHandler{ListCreatedEvent}" />
    public class ListEventHandlers : IEventHandler<ListCreatedEvent>
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
    }
}
