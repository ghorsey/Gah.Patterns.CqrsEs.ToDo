namespace Gah.Patterns.ToDo.Command.Domain.Events
{
    using System;
    using Gah.Blocks.CqrsEs.Events;

    /// <summary>
    /// Class <c>ListCreated</c>.
    /// Implements the <see cref="Gah.Blocks.CqrsEs.Events.BasicEvent" />
    /// </summary>
    /// <seealso cref="Gah.Blocks.CqrsEs.Events.BasicEvent" />
    public class ListCreated : BasicEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListCreated"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="title">The title.</param>
        public ListCreated(Guid id, string title)
        {
            this.Id = id;
            this.Title = title;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; private set; }
    }
}
