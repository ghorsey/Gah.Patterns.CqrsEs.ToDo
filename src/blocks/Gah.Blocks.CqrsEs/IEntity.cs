namespace Gah.Blocks.CqrsEs
{
    /// <summary>
    /// Interface <c>IEntity</c>
    /// </summary>
    /// <typeparam name="TId">The type of the identifier.</typeparam>
    public interface IEntity<out TId>
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        TId Id { get; }
    }
}
