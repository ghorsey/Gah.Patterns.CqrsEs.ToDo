namespace Gah.Blocks.CqrsEs.Events
{
    using System.Reflection;

    /// <summary>
    /// Class <c>BasicEvent</c>.
    /// Implements the <see cref="Gah.Blocks.CqrsEs.Events.IEvent" />
    /// </summary>
    /// <seealso cref="Gah.Blocks.CqrsEs.Events.IEvent" />
    public abstract class BasicEvent : IEvent
    {
        /// <summary>
        /// Gets the type of the event.
        /// </summary>
        /// <value>The type of the event.</value>
        public virtual string EventType =>
            $"{this.GetType().GetTypeInfo().FullName}, {this.GetType().Assembly.GetName()}";
    }
}