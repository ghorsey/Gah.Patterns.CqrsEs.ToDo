namespace Gah.Blocks.CqrsEs.EventStore.Sql
{
    using System;
    using System.Collections.Generic;

    using Gah.Blocks.CqrsEs.Events;
    using Gah.Blocks.CqrsEs.EventStore.Sql.Entities;

    using Newtonsoft.Json;

    /// <summary>
    /// Class <c>EventStoreExtensions</c>.
    /// </summary>
    internal static class EventStoreExtensions
    {
        /// <summary>
        /// The lock
        /// </summary>
        private static readonly object Lock = new object();

        /// <summary>
        /// The cache
        /// </summary>
        private static readonly Dictionary<string, Type> Cache = new Dictionary<string, Type>();

        /// <summary>
        /// Converts to event.
        /// </summary>
        /// <param name="sources">The sources.</param>
        /// <returns>A/an <c>IEnumerable&lt;IEvent&gt;</c>.</returns>
        public static IEnumerable<IEvent> ConvertToEvent(this IEnumerable<EventSource> sources)
        {
            foreach (var es in sources)
            {
                Type eventType;
                lock (Lock)
                {
                    Cache.TryGetValue(es.EventType, out eventType);
                }

                if (eventType == null)
                {
                    eventType = Type.GetType(es.EventType);
                    lock (Lock)
                    {
                        Cache.Add(es.EventType, eventType);
                    }
                }

                yield return (IEvent)JsonConvert.DeserializeObject(es.Event, eventType);
            }
        }
    }
}