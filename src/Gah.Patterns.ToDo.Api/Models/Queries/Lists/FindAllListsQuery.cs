namespace Gah.Patterns.ToDo.Api.Models.Queries.Lists
{
    using System.Collections.Generic;

    using Gah.Blocks.CqrsEs.Queries;
    using Gah.Patterns.Todo.Domain;

    /// <summary>
    /// Class <c>FindAllListsQuery</c>.
    /// Implements the <see cref="IQuery{TResponse}" />
    /// </summary>
    /// <seealso cref="IQuery{TResponse}" />
    public class FindAllListsQuery : IQuery<List<ToDoList>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindAllListsQuery"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        public FindAllListsQuery(string title)
        {
            this.Title = title;
        }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; }
    }
}
