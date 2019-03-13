namespace Gah.Blocks.CqrsEs.EventStore.Sql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Gah.Blocks.CqrsEs.Events;
    using Gah.Blocks.CqrsEs.EventStore.Sql.DbContexts;
    using Gah.Blocks.CqrsEs.EventStore.Sql.Entities;

    using Microsoft.EntityFrameworkCore;

    using Newtonsoft.Json;

    /// <summary>
    /// The event store.
    /// </summary>
    public class EventStore : IEventStore
    {
        /// <summary>
        /// The context.
        /// </summary>
        private readonly IEventStoreDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventStore"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EventStore(IEventStoreDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Reads the forward.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>A/an <c>Task&lt;ReadStreamResult&gt;</c>.</returns>
        public async Task<ReadStreamResult> ReadForwardAsync(string stream, long start, int count)
        {
            var sources = await this.context.EventSources
                              .Where(es => es.Stream.Equals(stream) && es.EventNumber >= start)
                              .OrderBy(es => es.EventNumber)
                              .Take(count)
                              .ToListAsync();

            var events = sources.ConvertToEvent();

            var countOfStream = this.context.EventSources.Count(es => es.Stream.Equals(stream));
            var last = sources.LastOrDefault()?.EventNumber ?? 0;

            return new ReadStreamResult(
                stream,
                ReadDirection.Forward,
                start,
                last,
                last + 1,
                countOfStream == last,
                events);
        }

        /// <summary>
        /// Reads the backward.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>A/an <c>Task&lt;ReadStreamResult&gt;</c>.</returns>
        public async Task<ReadStreamResult> ReadBackwardAsync(string stream, long start, int count)
        {
            var sources = await this.context.EventSources
                              .Where(es => es.Stream.Equals(stream) && es.EventNumber <= start)
                              .OrderByDescending(es => es.EventNumber)
                              .Take(count)
                              .ToListAsync();

            var events = sources.ConvertToEvent();

            var first = sources.FirstOrDefault()?.EventNumber ?? 0;

            return new ReadStreamResult(
                stream,
                ReadDirection.Backward,
                first,
                start,
                Math.Min(1, first - 1),
                first == 1,
                events);
        }

        /// <summary>
        /// Reads all forward.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>A/an <c>Task&lt;ReadStreamResult&gt;</c>.</returns>
        public async Task<ReadStreamResult> ReadAllForwardAsync(string stream)
        {
            var sources = await this.context.EventSources.Where(es => es.Stream.Equals(stream))
                              .OrderBy(es => es.EventNumber)
                              .ToListAsync();
            var events = sources.ConvertToEvent().ToList();

            return new ReadStreamResult(
                stream,
                ReadDirection.Forward,
                1,
                events.Count,
                events.Count + 1,
                true,
                events);
        }

        /// <summary>
        /// Reads all backward.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>A/an <c>Task&lt;ReadStreamResult&gt;</c>.</returns>
        public async Task<ReadStreamResult> ReadAllBackwardAsync(string stream)
        {
            var sources = await this.context.EventSources.Where(es => es.Stream.Equals(stream))
                              .OrderByDescending(es => es.EventNumber)
                              .ToListAsync();
            var events = sources.ConvertToEvent().ToList();

            return new ReadStreamResult(
                stream,
                ReadDirection.Forward,
                1,
                events.Count,
                events.Count + 1,
                true,
                events);
        }

        /// <summary>
        /// Appends to stream asynchronous.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="expectedVersion">The expected version.</param>
        /// <param name="events">The events.</param>
        /// <returns>A/an <c>Task&lt;WriteResult&gt;</c>.</returns>
        public async Task<WriteResult> AppendToStreamAsync(string stream, long expectedVersion, IEnumerable<IEvent> events)
        {
            var eventSources = new List<EventSource>();
            foreach (var @event in events)
            {
                eventSources.Add(
                    new EventSource(
                        Guid.NewGuid(),
                        stream,
                        expectedVersion++,
                        JsonConvert.SerializeObject(@event),
                        @event.EventType));
            }

            await this.context.EventSources.AddRangeAsync(eventSources);
            await this.context.SaveChangesAsync();

            return new WriteResult(expectedVersion);
        }

        /// <summary>
        /// Appends to stream asynchronous.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="expectedVersion">The expected version.</param>
        /// <param name="events">The events.</param>
        /// <returns>A/an <c>Task&lt;WriteResult&gt;</c>.</returns>
        public Task<WriteResult> AppendToStreamAsync(string stream, long expectedVersion, params IEvent[] events)
        {
            return this.AppendToStreamAsync(stream, expectedVersion, events.AsEnumerable());
        }
    }
}