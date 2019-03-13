namespace Gah.Patterns.ToDo.Commands.Domain.Commands.Items
{
    using System;

    using Gah.Blocks.CqrsEs.Commands;

    /// <summary>
    /// Class <c>UpdateItemCommand</c>.
    /// </summary>
    public class UpdateItemCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateItemCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="listId">The list identifier.</param>
        /// <param name="title">The title.</param>
        public UpdateItemCommand(Guid id, Guid listId, string title)
        {
            this.Id = id;
            this.ListId = listId;
            this.Title = title;
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
    }
}