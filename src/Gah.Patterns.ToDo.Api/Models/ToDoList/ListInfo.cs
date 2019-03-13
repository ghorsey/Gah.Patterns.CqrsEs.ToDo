namespace Gah.Patterns.ToDo.Api.Models.ToDoList
{
    /// <summary>
    /// Class <c>CreateListInfo</c>.
    /// </summary>
    public class ListInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListInfo"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        public ListInfo(string title)
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
