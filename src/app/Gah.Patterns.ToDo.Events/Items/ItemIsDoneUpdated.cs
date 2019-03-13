namespace Gah.Patterns.ToDo.Events.Items
{
    using System;

    using Gah.Blocks.CqrsEs.Events;

    /// <summary>
    /// Class <c>ItemToDoUpdated</c>.
    /// Implements the <see cref="Gah.Blocks.CqrsEs.Events.BasicEvent" />
    /// </summary>
    /// <seealso cref="Gah.Blocks.CqrsEs.Events.BasicEvent" />
    public class ItemIsDoneUpdated : BasicEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemIsDoneUpdated"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="listId">The list identifier.</param>
        /// <param name="isDone">if set to <c>true</c> [to do].</param>
        /// <param name="updated">The updated.</param>
        public ItemIsDoneUpdated(Guid id, Guid listId, bool isDone, DateTime updated)
        {
            this.Id = id;
            this.ListId = listId;
            this.IsDone = isDone;
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
        /// Gets a value indicating whether [to do].
        /// </summary>
        /// <value><c>true</c> if [to do]; otherwise, <c>false</c>.</value>
        public bool IsDone { get; }

        /// <summary>
        /// Gets the updated.
        /// </summary>
        /// <value>The updated.</value>
        public DateTime Updated { get; }
    }
}