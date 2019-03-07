namespace Gah.Blocks.CqrsEs.Events
{
    using MediatR;

    /// <summary>
    /// Interface <c>IEvent</c>
    /// Implements the <see cref="MediatR.INotification" /></summary>
    /// <seealso cref="MediatR.INotification" />
    public interface IEvent : INotification
    {
        /// <summary>
        /// Gets the type of the event.
        /// </summary>
        /// <value>The type of the event.</value>
        string EventType { get; }
    }
}
