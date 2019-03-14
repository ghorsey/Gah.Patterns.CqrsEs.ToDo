namespace Gah.Patterns.ToDo.Events.Lists
{
    using System;

    using Gah.Blocks.CqrsEs.Events;

    /// <summary>
    /// Class <c>ListDeletedEvent</c>.
    /// </summary>
    public class ListDeletedEvent : BasicEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListDeletedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public ListDeletedEvent(Guid id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; }
    }
}