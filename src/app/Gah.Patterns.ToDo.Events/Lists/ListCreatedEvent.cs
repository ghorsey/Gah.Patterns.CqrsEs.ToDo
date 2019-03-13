namespace Gah.Patterns.ToDo.Events.Lists
{
    using System;

    using Gah.Blocks.CqrsEs.Events;

    /// <summary>
    /// Class <c>ListCreated</c>.
    /// Implements the <see cref="Gah.Blocks.CqrsEs.Events.BasicEvent" />
    /// </summary>
    /// <seealso cref="Gah.Blocks.CqrsEs.Events.BasicEvent" />
    public class ListCreatedEvent : BasicEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListCreatedEvent" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="title">The title.</param>
        /// <param name="updated">The updated.</param>
        /// <param name="created">The created.</param>
        public ListCreatedEvent(Guid id, string title, DateTime updated, DateTime created)
        {
            this.Id = id;
            this.Title = title;
            this.Updated = updated;
            this.Created = created;
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

        /// <summary>
        /// Gets the updated.
        /// </summary>
        /// <value>The updated.</value>
        public DateTime Updated { get; private set; }

        /// <summary>
        /// Gets the created.
        /// </summary>
        /// <value>The created.</value>
        public DateTime Created { get; private set; }
    }
}