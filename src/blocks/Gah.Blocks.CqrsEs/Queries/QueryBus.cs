namespace Gah.Blocks.CqrsEs.Queries
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class <c>QueryBus</c>.
    /// </summary>
    public class QueryBus : IQueryBus
    {
        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator mediator;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<QueryBus> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBus"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="logger">The logger.</param>
        public QueryBus(IMediator mediator, ILogger<QueryBus> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <typeparam name="TQuery">The type of the t query.</typeparam>
        /// <typeparam name="TResponse">The type of the t response.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task&lt;TResponse&gt;</c>.</returns>
        public Task<TResponse> ExecuteAsync<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken = default(CancellationToken))
            where TQuery : IQuery<TResponse>
        {
            this.logger.LogDebug("Executing query {@query}", query);

            return this.mediator.Send(query, cancellationToken);
        }
    }
}