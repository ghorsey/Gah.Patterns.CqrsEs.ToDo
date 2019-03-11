namespace Gah.Patterns.ToDo.Api.Models.Queries.Lists
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Gah.Blocks.CqrsEs.Queries;
    using Gah.Patterns.ToDo.Query.Domain;
    using Gah.Patterns.ToDo.Query.Repository;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The find all lists handler.
    /// </summary>
    public class ListsQueryHandler
        : IQueryHandler<FindAllListsQuery, List<ToDoList>>,
          IQueryHandler<FindList, ToDoList>
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
            this.logger.LogDebug("Fetching lists with query: '{@request}'", request);

            return this.repository.FindListsAsync(request.Title);
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task&lt;ToDoList&gt;</c>.</returns>
        public Task<ToDoList> Handle(FindList request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Fetch a single list by id {id}", request.Id);
            return this.repository.FindListAsync(request.Id);
        }
    }
}