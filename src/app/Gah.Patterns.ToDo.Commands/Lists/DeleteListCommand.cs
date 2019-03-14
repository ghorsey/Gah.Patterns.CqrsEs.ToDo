namespace Gah.Patterns.ToDo.Commands.Lists
{
    using System;

    using Gah.Blocks.CqrsEs.Commands;

    /// <summary>
    /// Class <c>DeleteListCommand</c>.
    /// </summary>
    public class DeleteListCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteListCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public DeleteListCommand(Guid id)
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