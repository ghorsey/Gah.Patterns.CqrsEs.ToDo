namespace Gah.Patterns.ToDo.Api.Models.Queries.Lists
{
    using System;

    using Gah.Blocks.CqrsEs.Queries;
    using Gah.Patterns.ToDo.Query.Domain;

    /// <summary>
    /// Class <c>FindList</c>.
    /// </summary>
    public class FindList : IQuery<ToDoList>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindList"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public FindList(Guid id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public Guid Id { get; }
    }
}