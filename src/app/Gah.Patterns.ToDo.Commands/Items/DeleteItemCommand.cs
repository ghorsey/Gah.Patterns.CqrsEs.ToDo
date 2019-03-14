namespace Gah.Patterns.ToDo.Commands.Items
{
    using System;

    using Gah.Blocks.CqrsEs.Commands;

    /// <summary>
    /// Class <c>DeleteItemCommand</c>.
    /// </summary>
    public class DeleteItemCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteItemCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="listId">The list identifier.</param>
        public DeleteItemCommand(Guid id, Guid listId)
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