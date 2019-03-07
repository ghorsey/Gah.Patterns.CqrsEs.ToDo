namespace Gah.Patterns.ToDo.Domain
{
    using System;

    /// <summary>
    /// Basic Class
    /// </summary>
    public class ToDoList
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoList"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="title">The title.</param>
        /// <param name="totalItems">The total items.</param>
        /// <param name="totalCompleted">The total completed.</param>
        /// <param name="totalPending">The total pending.</param>
        /// <param name="updated">The updated.</param>
        /// <param name="created">The created.</param>
        public ToDoList(Guid id, string title, int totalItems, int totalCompleted, int totalPending, DateTime updated, DateTime created)
        {
            this.Id = id;
            this.Title = title;
            this.TotalItems = totalItems;
            this.TotalCompleted = totalCompleted;
            this.TotalPending = totalPending;
            this.Updated = updated;
            this.Created = created;
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

        /// <summary>
        /// Gets the total items.
        /// </summary>
        /// <value>The total items.</value>
        public int TotalItems { get; }

        /// <summary>
        /// Gets the total completed.
        /// </summary>
        /// <value>The total completed.</value>
        public int TotalCompleted { get; }

        /// <summary>
        /// Gets the total pending.
        /// </summary>
        /// <value>The total pending.</value>
        public int TotalPending { get; }

        /// <summary>
        /// Gets the created.
        /// </summary>
        /// <value>The created.</value>
        public DateTime Created { get; }

        /// <summary>
        /// Gets the updated.
        /// </summary>
        /// <value>The updated.</value>
        public DateTime Updated { get; }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
