namespace Gah.Patterns.ToDo.Events.Items
{
    using System;

    using Gah.Blocks.CqrsEs.Events;

    /// <summary>
    /// Class <c>ItemDeletedEvent</c>.
    /// </summary>
    public class ItemDeletedEvent : BasicEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemDeletedEvent" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="listId">The list identifier.</param>
        public ItemDeletedEvent(Guid id, Guid listId)
        {
            this.Id = id;
            this.ListId = listId;
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
    }
}