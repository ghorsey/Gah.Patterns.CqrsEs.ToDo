namespace Gah.Patterns.ToDo.Command.Domain.Commands.Lists
{
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.CqrsEs.Commands;
    using Gah.Blocks.CqrsEs.Events;
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
        /// The event bus
        /// </summary>
        private IEventBus eventBus;

        /// <summary>
        /// The logger
        /// </summary>
        private ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListCommandHandler"/> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="logger">The logger.</param>
        public ListCommandHandler(IEventBus eventBus, ILogger<ListCommandHandler> logger)
        {
            this.eventBus = eventBus;
            this.logger = logger;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task&lt;Unit&gt;</c>.</returns>
        public Task<Unit> Handle(CreateListCommand request, CancellationToken cancellationToken)
        {
            // todo: this method should be trying to load the list from the event stream, then verifying that it
            // doesn't already exist... it must be a new object....
            return Task.FromResult(Unit.Value);
        }
    }
}