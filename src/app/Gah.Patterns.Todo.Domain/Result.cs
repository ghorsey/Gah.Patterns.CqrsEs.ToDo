namespace Gah.Patterns.Todo.Domain
{
    /// <summary>
    /// The standard API result wrapper
    /// </summary>
    /// <typeparam name="T">The type of the value</typeparam>
    public sealed class Result<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Result(T value)
        {
            this.Value = value;
            this.Success = true;
            this.Message = string.Empty;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Result{T}"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public T Value { get; }
    }
}
