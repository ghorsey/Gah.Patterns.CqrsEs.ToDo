namespace Gah.Patterns.ToDo.Commands.Lists
{
    using System;

    using Gah.Blocks.CqrsEs.Commands;

    /// <summary>
    /// Class <c>UpdateListCommand</c>.
    /// </summary>
    public class UpdateListCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateListCommand" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="title">The title.</param>
        public UpdateListCommand(Guid id, string title)
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
