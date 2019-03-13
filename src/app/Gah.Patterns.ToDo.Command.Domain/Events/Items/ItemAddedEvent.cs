namespace Gah.Patterns.ToDo.Command.Domain.Events.Items
{
    using System;

    using Gah.Blocks.CqrsEs.Events;

    /// <summary>
    /// Class <c>ItemAddedEvent</c>.
    /// Implements the <see cref="BasicEvent" />
    /// </summary>
    /// <seealso cref="BasicEvent" />
    public class ItemAddedEvent : BasicEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemAddedEvent" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="listId">The list identifier.</param>
        /// <param name="title">The title.</param>
        /// <param name="created">The created.</param>
        /// <param name="updated">The updated.</param>
        public ItemAddedEvent(Guid id, Guid listId, string title, DateTime created, DateTime updated)
        {
            this.Id = id;
            this.ListId = listId;
            this.Title = title;
            this.Created = created;
            this.Updated = updated;
        }

        /// <summary>
        /// Gets the list identifier.
        /// </summary>
        /// <value>The list identifier.</value>
        public Guid ListId { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; }

        /// <summary>
        /// Gets the created date
        /// </summary>
        /// <value>The created date.</value>
        public DateTime Created { get; }

        /// <summary>
        /// Gets the updated date.
        /// </summary>
        /// <value>The updated date.</value>
        public DateTime Updated { get; }
    }
}
