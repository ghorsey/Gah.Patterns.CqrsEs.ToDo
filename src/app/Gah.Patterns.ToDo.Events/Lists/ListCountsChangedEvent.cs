namespace Gah.Patterns.ToDo.Events.Lists
{
    using System;

    using Gah.Blocks.CqrsEs.Events;

    /// <summary>
    /// Class <c>ListCountsChanged</c>.
    /// </summary>
    public class ListCountsChangedEvent : BasicEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListCountsChangedEvent" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="totalItems">The total items.</param>
        /// <param name="pendingItems">The pending items.</param>
        /// <param name="completedItems">The completed items.</param>
        public ListCountsChangedEvent(Guid id, int totalItems, int pendingItems, int completedItems)
        {
            this.Id = id;
            this.TotalItems = totalItems;
            this.PendingItems = pendingItems;
            this.CompletedItems = completedItems;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; }

        /// <summary>
        /// Gets the total items.
        /// </summary>
        /// <value>The total items.</value>
        public int TotalItems { get; }

        /// <summary>
        /// Gets the pending items.
        /// </summary>
        /// <value>The pending items.</value>
        public int PendingItems { get; }

        /// <summary>
        /// Gets the completed items.
        /// </summary>
        /// <value>The completed items.</value>
        public int CompletedItems { get; }
    }
}