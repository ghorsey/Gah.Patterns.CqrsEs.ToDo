namespace Gah.Blocks.CqrsEs.EventStore
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Gah.Blocks.CqrsEs.Events;

    /// <summary>
    /// Interface <c>IEventStore</c>
    /// </summary>
    public interface IEventStore
    {
        /// <summary>
        /// Reads the forward.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>A/an <c>Task&lt;ReadStreamResult&gt;</c>.</returns>
        Task<ReadStreamResult> ReadForwardAsync(string stream, long start, int count);

        /// <summary>
        /// Reads the backward.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>A/an <c>Task&lt;ReadStreamResult&gt;</c>.</returns>
        Task<ReadStreamResult> ReadBackwardAsync(string stream, long start, int count);

        /// <summary>
        /// Reads all forward.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>A/an <c>Task&lt;ReadStreamResult&gt;</c>.</returns>
        Task<ReadStreamResult> ReadAllForwardAsync(string stream);

        /// <summary>
        /// Reads all backward.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>A/an <c>Task&lt;ReadStreamResult&gt;</c>.</returns>
        Task<ReadStreamResult> ReadAllBackwardAsync(string stream);

        /// <summary>
        /// Appends to stream asynchronous.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="expectedVersion">The expected version.</param>
        /// <param name="events">The events.</param>
        /// <returns>A/an <c>Task&lt;WriteResult&gt;</c>.</returns>
        Task<WriteResult> AppendToStreamAsync(
            string stream,
            long expectedVersion,
            IEnumerable<IEvent> events);

        /// <summary>
        /// Appends to stream asynchronous.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="expectedVersion">The expected version.</param>
        /// <param name="events">The events.</param>
        /// <returns>A/an <c>Task&lt;WriteResult&gt;</c>.</returns>
        Task<WriteResult> AppendToStreamAsync(
            string stream,
            long expectedVersion,
            params IEvent[] events);
    }
}
