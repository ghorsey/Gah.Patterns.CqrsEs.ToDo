namespace Gah.Blocks.CqrsEs.EventStore.InMemory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Gah.Blocks.CqrsEs.Events;

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The in memory event store.
    /// </summary>
    public class InMemoryEventStore : IEventStore
    {
        /// <summary>
        /// The LCK
        /// </summary>
        private static readonly object Lck = new object();

        /// <summary>
        /// The items
        /// </summary>
        private static readonly Dictionary<string, List<InMemoryEvent>> Items =
            new Dictionary<string, List<InMemoryEvent>>();

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryEventStore"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public InMemoryEventStore(ILogger<InMemoryEvent> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Reads the forward.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>A/an <c>Task&lt;ReadStreamResult&gt;</c>.</returns>
        public Task<ReadStreamResult> ReadForwardAsync(string stream, long start, int count)
        {
            if (!Items.ContainsKey(stream))
            {
                this.logger.LogDebug("The event stream does not contain the stream {stream}", stream);
                return Task.FromResult(
                    new ReadStreamResult(
                        stream,
                        ReadDirection.Forward,
                        start,
                        start,
                        -1,
                        true,
                        new IEvent[0]));
            }

            var streamData = Items[stream];
            var events = streamData.Where(e => e.EventNumber >= start)
                .OrderBy(e => e.EventNumber)
                .Take(count)
                .ToList();

            var lastEventNumber = events.Last().EventNumber + 1;
            var isEndOfStream = streamData.Last().EventNumber == events.Last().EventNumber;

            return Task.FromResult(
                new ReadStreamResult(
                    stream,
                    ReadDirection.Forward,
                    start,
                    lastEventNumber + 1,
                    lastEventNumber,
                    isEndOfStream,
                    events.Select(e => e.Event)));
        }

        /// <summary>
        /// Reads the backward.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>A/an <c>Task&lt;ReadStreamResult&gt;</c>.</returns>
        public Task<ReadStreamResult> ReadBackwardAsync(string stream, long start, int count)
        {
            if (!Items.ContainsKey(stream))
            {
                this.logger.LogDebug("The event stream does not contain the stream {stream}", stream);
                return Task.FromResult(
                    new ReadStreamResult(
                        stream,
                        ReadDirection.Backward,
                        start,
                        start,
                        -1,
                        true,
                        new IEvent[0]));
            }

            var streamData = Items[stream];
            var events = streamData.Where(e => e.EventNumber <= start)
                .OrderByDescending(e => e.EventNumber)
                .Take(count)
                .ToList();

            long lastEventNumber = events.Last().EventNumber + 1;
            var isEndOfStream = streamData.Last().EventNumber == events.Last().EventNumber;

            return Task.FromResult(
                new ReadStreamResult(
                    stream,
                    ReadDirection.Backward,
                    start,
                    lastEventNumber + 1,
                    lastEventNumber,
                    isEndOfStream,
                    events.Select(e => e.Event)));
        }

        /// <summary>
        /// Reads all forward.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>A/an <c>Task&lt;ReadStreamResult&gt;</c>.</returns>
        public Task<ReadStreamResult> ReadAllForwardAsync(string stream)
        {
            if (!Items.ContainsKey(stream))
            {
                this.logger.LogDebug("The event stream does not contain the stream {stream}", stream);
                return Task.FromResult(
                    new ReadStreamResult(
                        stream,
                        ReadDirection.Forward,
                        0,
                        0,
                        -1,
                        true,
                        new IEvent[0]));
            }

            var streamData = Items[stream];
            var events = streamData.OrderBy(e => e.EventNumber).ToList();

            long lastEventNumber = events.Last().EventNumber + 1;
            var isEndOfStream = streamData.Last().EventNumber == events.Last().EventNumber;

            return Task.FromResult(
                new ReadStreamResult(
                    stream,
                    ReadDirection.Forward,
                    events.First().EventNumber,
                    lastEventNumber + 1,
                    lastEventNumber,
                    isEndOfStream,
                    events.Select(e => e.Event)));
        }

        /// <summary>
        /// Reads all backward.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>A/an <c>Task&lt;ReadStreamResult&gt;</c>.</returns>
        public Task<ReadStreamResult> ReadAllBackwardAsync(string stream)
        {
            if (!Items.ContainsKey(stream))
            {
                this.logger.LogDebug("The event store does not contain the stream {stream}", stream);
                return Task.FromResult(
                    new ReadStreamResult(
                        stream,
                        ReadDirection.Backward,
                        0,
                        0,
                        -1,
                        true,
                        new IEvent[0]));
            }

            var streamData = Items[stream];
            var events = streamData.OrderByDescending(e => e.EventNumber).ToList();

            long lastEventNumber = events.Last().EventNumber + 1;
            var isEndOfStream = streamData.Last().EventNumber == events.Last().EventNumber;

            return Task.FromResult(
                new ReadStreamResult(
                    stream,
                    ReadDirection.Backward,
                    events.First().EventNumber,
                    lastEventNumber + 1,
                    lastEventNumber,
                    isEndOfStream,
                    events.Select(e => e.Event)));
        }

        /// <summary>
        /// Appends to stream asynchronous.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="expectedVersion">The expected version.</param>
        /// <param name="events">The events.</param>
        /// <returns>A/an <c>Task&lt;WriteResult&gt;</c>.</returns>
        public Task<WriteResult> AppendToStreamAsync(string stream, long expectedVersion, IEnumerable<IEvent> events)
        {
            lock (Lck)
            {
                if (!Items.ContainsKey(stream))
                {
                    this.logger.LogDebug("Creating stream {stream}", stream);
                    Items.Add(stream, new List<InMemoryEvent>());
                }

                if (Items[stream].All(e => e.EventNumber != expectedVersion))
                {
                    foreach (var @event in events)
                    {
                        this.logger.LogDebug(
                            "Adding event number {eventNumber} with event {@event}",
                            expectedVersion,
                            @event);

                        Items[stream]
                            .Add(
                                new InMemoryEvent(
                                    Guid.NewGuid(),
                                    stream,
                                    expectedVersion++,
                                    @event));
                    }
                }
            }

            return Task.FromResult(new WriteResult(expectedVersion));
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
