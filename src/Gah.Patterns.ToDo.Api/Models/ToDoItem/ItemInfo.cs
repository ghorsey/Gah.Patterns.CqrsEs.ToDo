namespace Gah.Patterns.ToDo.Api.Models.ToDoItem
{
    /// <summary>
    /// Class <c>ItemInfo</c>.
    /// </summary>
    public class ItemInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemInfo"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        public ItemInfo(string title)
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