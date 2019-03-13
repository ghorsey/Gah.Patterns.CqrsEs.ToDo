namespace Gah.Patterns.ToDo.Command.Domain.Commands.Items
{
    using System;

    using Gah.Blocks.CqrsEs.Commands;

    /// <summary>
    /// Class <c>UpdateToDoCommand</c>.
    /// </summary>
    public class UpdateItemIsDoneCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateItemIsDoneCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="listId">The list identifier.</param>
        /// <param name="isDone">if set to <c>true</c> [to do].</param>
        public UpdateItemIsDoneCommand(Guid id, Guid listId, bool isDone)
        {
            this.Id = id;
            this.ListId = listId;
            this.IsDone = isDone;
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
    }
}