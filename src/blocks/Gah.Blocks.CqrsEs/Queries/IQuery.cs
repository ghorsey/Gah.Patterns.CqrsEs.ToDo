namespace Gah.Blocks.CqrsEs.Queries
{
    using MediatR;

    /// <summary>
    /// Interface <c>IQuery</c>
    /// Implements the <see cref="MediatR.IRequest{TResponse}" />
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <seealso cref="MediatR.IRequest{TResponse}" />
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
