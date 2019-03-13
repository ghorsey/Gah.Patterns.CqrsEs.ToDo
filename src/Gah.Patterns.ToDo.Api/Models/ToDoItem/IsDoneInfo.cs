namespace Gah.Patterns.ToDo.Api.Models.ToDoItem
{
    /// <summary>
    /// Class <c>IsDoneInfo</c>.
    /// </summary>
    public class IsDoneInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IsDoneInfo"/> class.
        /// </summary>
        /// <param name="isDone">if set to <c>true</c> [is done].</param>
        public IsDoneInfo(bool isDone)
        {
            this.IsDone = isDone;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is done.
        /// </summary>
        /// <value><c>true</c> if this instance is done; otherwise, <c>false</c>.</value>
        public bool IsDone { get; set; }
    }
}