namespace Gah.Patterns.ToDo.Command.Domain.Events.Items
{
    using System;

    using Gah.Blocks.CqrsEs.Events;

    /// <summary>
    /// Class <c>ItemUpdatedEvent</c>.
    /// Implements the <see cref="Gah.Blocks.CqrsEs.Events.BasicEvent" />
    /// </summary>
    /// <seealso cref="Gah.Blocks.CqrsEs.Events.BasicEvent" />
    public class ItemUpdatedEvent : BasicEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemUpdatedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="listId">The list identifier.</param>
        /// <param name="title">The title.</param>
        /// <param name="updated">The updated.</param>
        public ItemUpdatedEvent(Guid id, Guid listId, string title, DateTime updated)
        {
            this.Id = id;
            this.ListId = listId;
            this.Title = title;
            this.Updated = updated;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; }

        /// <summary>
        /// Gets the list identifier.
        /// </summary>
        /// <value>The list identifier.</value>
        public Guid ListId { get; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; }

        /// <summary>
        /// Gets the updated.
        /// </summary>
        /// <value>The updated.</value>
        public DateTime Updated { get; }
    }
}