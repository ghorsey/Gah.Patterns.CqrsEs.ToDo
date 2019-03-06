namespace Gah.Blocks.CqrsEs.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The command bus.
    /// </summary>
    public class CommandBus : ICommandBus
    {
        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator mediator;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<CommandBus> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandBus" /> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="logger">The logger.</param>
        public CommandBus(IMediator mediator, ILogger<CommandBus> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <typeparam name="TCommand">The type of the t command.</typeparam>
        /// <param name="command">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        public Task Execute<TCommand>(TCommand command, CancellationToken cancellationToken = default(CancellationToken))
            where TCommand : ICommand
        {
            this.logger.LogDebug("Executing {@command}", command);
            return this.mediator.Send(command, cancellationToken);
        }
    }
}