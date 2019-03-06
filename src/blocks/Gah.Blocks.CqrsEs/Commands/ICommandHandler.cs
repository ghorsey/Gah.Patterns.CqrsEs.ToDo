namespace Gah.Blocks.CqrsEs.Commands
{
    using MediatR;

    /// <summary>
    /// The CommandHandler interface.
    /// Implements the <see cref="IRequestHandler{TCommand}" />
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    /// <seealso cref="IRequestHandler{TCommand}" />
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
        where TCommand : ICommand
    {
    }
}