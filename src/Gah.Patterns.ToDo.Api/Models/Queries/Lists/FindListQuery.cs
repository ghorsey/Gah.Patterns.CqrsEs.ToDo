namespace Gah.Patterns.ToDo.Api.Models.Queries.Lists
{
    using System;

    using Gah.Blocks.CqrsEs.Queries;
    using Gah.Patterns.ToDo.Query.Domain;

    /// <summary>
    /// Class <c>FindList</c>.
    /// </summary>
    public class FindListQuery : IQuery<ToDoList>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindListQuery"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public FindListQuery(Guid id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public Guid Id { get; }
    }
}