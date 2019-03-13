namespace Gah.Patterns.ToDo.Api.Models.Queries.Items
{
    using System;

    using Gah.Blocks.CqrsEs.Queries;
    using Gah.Patterns.ToDo.Query.Domain;

    /// <summary>
    /// Class <c>FindItemQuery</c>.
    /// Implements the <see cref="IQuery{ToDoItem}" />
    /// </summary>
    /// <seealso cref="IQuery{ToDoItem}" />
    public class FindItemQuery : IQuery<ToDoItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindItemQuery"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public FindItemQuery(Guid id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; }
    }
}