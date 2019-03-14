namespace Gah.Patterns.ToDo.Query.Queries.Items
{
    using System;
    using System.Collections.Generic;

    using Gah.Blocks.CqrsEs.Queries;
    using Gah.Patterns.ToDo.Query.Domain;

    /// <summary>
    /// Class <c>FindAllItemsQuery</c>.
    /// Implements the <see cref="IQuery{TResponse}" />
    /// </summary>
    /// <seealso cref="IQuery{T}" />
    public class FindAllItemsQuery : IQuery<List<ToDoItem>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindAllItemsQuery"/> class.
        /// </summary>
        /// <param name="listId">The list identifier.</param>
        public FindAllItemsQuery(Guid listId)
        {
            this.ListId = listId;
        }

        /// <summary>
        /// Gets the list identifier.
        /// </summary>
        /// <value>The list identifier.</value>
        public Guid ListId { get; }
    }
}
