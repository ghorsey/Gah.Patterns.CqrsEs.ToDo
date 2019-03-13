namespace Gah.Patterns.ToDo.Command.Domain.Commands.Items
{
    using System;

    using Gah.Blocks.CqrsEs.Commands;

    /// <summary>
    /// Class <c>CreateItemCommand</c>.
    /// Implements the <see cref="ICommand" />
    /// </summary>
    /// <seealso cref="ICommand" />
    public class CreateItemCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateItemCommand" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="listId">The list identifier.</param>
        /// <param name="title">The title.</param>
        public CreateItemCommand(Guid id, Guid listId, string title)
        {
            this.Id = id;
            this.ListId = listId;
            this.Title = title;
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
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
