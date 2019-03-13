namespace Gah.Patterns.ToDo.Command.Domain.Events
{
    using System;

    using Gah.Blocks.CqrsEs.Events;

    /// <summary>
    /// Class <c>ListTitleUpdated</c>.
    /// </summary>
    public class ListUpdatedEvent : BasicEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListUpdatedEvent" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="title">The title.</param>
        /// <param name="updated">The updated.</param>
        public ListUpdatedEvent(Guid id, string title, DateTime updated)
        {
            this.Id = id;
            this.Title = title;
            this.Updated = updated;
        }

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
        /// Gets the updated.
        /// </summary>
        /// <value>The updated.</value>
        public DateTime Updated { get; }
    }
}