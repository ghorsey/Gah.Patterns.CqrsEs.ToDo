namespace Gah.Patterns.ToDo.Api.Models.Queries.Lists
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Gah.Blocks.CqrsEs.Queries;
    using Gah.Patterns.ToDo.Domain;
    using Gah.Patterns.ToDo.Repository;

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The find all lists handler.
    /// </summary>
    public class ListsQueryHandler : IQueryHandler<FindAllListsQuery, List<ToDoList>>
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// The repository
        /// </summary>
        private IToDoListRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListsQueryHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="logger">The logger.</param>
        public ListsQueryHandler(IToDoListRepository repository, ILogger<ListsQueryHandler> logger)
        {
            this.logger = logger;
            this.repository = repository;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task&lt;List&lt;ToDoList&gt;&gt;</c>.</returns>
        public Task<List<ToDoList>> Handle(FindAllListsQuery request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Fetching lists with query: '{@request}'");

            return this.repository.FindListsAsync(request.Title);
        }
    }
}