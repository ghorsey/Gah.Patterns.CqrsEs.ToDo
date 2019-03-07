namespace Gah.Blocks.CqrsEs.Queries
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface <c>IQueryBus</c>
    /// </summary>
    public interface IQueryBus
    {
        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <typeparam name="TQuery">The type of the t query.</typeparam>
        /// <typeparam name="TResponse">The type of the t response.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task&lt;TResponse&gt;</c>.</returns>
        Task<TResponse> ExecuteAsync<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken = default(CancellationToken))
            where TQuery : IQuery<TResponse>;
    }
}