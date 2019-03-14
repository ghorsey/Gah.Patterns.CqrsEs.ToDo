namespace Gah.Patterns.ToDo.Query.QueryHandlers
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Gah.Blocks.CqrsEs.Queries;
    using Gah.Patterns.ToDo.Query.Domain;
    using Gah.Patterns.ToDo.Query.Queries.Items;
    using Gah.Patterns.ToDo.Query.Repository;

    /// <summary>
    /// Class <c>ItemsQueryHandler</c>.
    /// Implements the <see cref="IQueryHandler{TQuery,TResponse}" />
    /// Implements the <see cref="IQueryHandler{FindItemQuery, ToDoItem}" />
    /// </summary>
    /// <seealso cref="IQueryHandler{FindItemQuery, ToDoItem}" />
    /// <seealso cref="IQueryHandler{TQuery, TResponse}" />
    public class ItemsQueryHandler : IQueryHandler<FindAllItemsQuery, List<ToDoItem>>, IQueryHandler<FindItemQuery, ToDoItem>
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IToDoItemRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemsQueryHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ItemsQueryHandler(IToDoItemRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task&lt;List&lt;ToDoItem&gt;&gt;</c>.</returns>
        public Task<List<ToDoItem>> Handle(FindAllItemsQuery request, CancellationToken cancellationToken)
        {
            return this.repository.FindItemsAsync(request.ListId);
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A/an <c>Task&lt;ToDoItem&gt;</c>.</returns>
        public Task<ToDoItem> Handle(FindItemQuery request, CancellationToken cancellationToken)
        {
            return this.repository.FindItemAsync(request.Id);
        }
    }
}