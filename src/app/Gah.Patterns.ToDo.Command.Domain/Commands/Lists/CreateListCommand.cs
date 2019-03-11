namespace Gah.Patterns.ToDo.Command.Domain.Commands.Lists
{
    using System;
    using Gah.Blocks.CqrsEs.Commands;

    /// <summary>
    /// Class <c>CreateListCommand</c>.
    /// </summary>
    public class CreateListCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateListCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="title">The title.</param>
        public CreateListCommand(Guid id, string title)
        {
            this.Id = id;
            this.Title = title;
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
    }
}
