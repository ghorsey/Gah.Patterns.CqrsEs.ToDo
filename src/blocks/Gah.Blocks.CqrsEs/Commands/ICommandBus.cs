namespace Gah.Blocks.CqrsEs.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface <c>ICommandBus</c>
    /// </summary>
    public interface ICommandBus
    {
        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <typeparam name="TCommand">The type of the t command.</typeparam>
        /// <param name="command">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task</c>.</returns>
        Task Execute<TCommand>(TCommand command, CancellationToken cancellationToken = default(CancellationToken))
            where TCommand : ICommand;
    }
}