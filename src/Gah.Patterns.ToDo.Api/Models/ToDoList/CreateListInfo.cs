namespace Gah.Patterns.ToDo.Api.Models.ToDoList
{
    /// <summary>
    /// Class <c>CreateListInfo</c>.
    /// </summary>
    public class CreateListInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateListInfo"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        public CreateListInfo(string title)
        {
            this.Title = title;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }
    }
}
