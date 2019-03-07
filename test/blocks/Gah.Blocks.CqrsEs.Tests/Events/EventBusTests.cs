namespace Gah.Blocks.CqrsEs.Tests.Events
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.CqrsEs.Events;
    using MediatR;

    using Microsoft.Extensions.Logging;

    using Moq;

    using Xunit;

    /// <summary>
    /// Class CommandBusTests.
    /// </summary>
    public class EventBusTests
    {
        /// <summary>
        /// Defines the test method ExecuteAsyncMethodTest.
        /// </summary>
        /// <returns>A/an <c>Task.</c></returns>
        [Fact]
        public async Task ExecuteAsyncMethodTest()
        {
            var events = new List<EventStub>
                           {
                               new EventStub()
                           };

            var cancellationToken = new CancellationToken(false);

            var mediatorMock = new Mock<IMediator>(MockBehavior.Strict);
            mediatorMock.Setup(_ => _.Publish(events[0], cancellationToken)).Returns(Task.CompletedTask);

            var loggerMock = new Mock<ILogger<EventBus>>(MockBehavior.Loose);

            var bus = new EventBus(mediatorMock.Object, loggerMock.Object);

            await bus.PublishAsync(events, cancellationToken);

            mediatorMock.Verify(_ => _.Publish(events[0], cancellationToken), Times.Once);
        }

        /// <summary>
        /// Class EventStub.
        /// Implements the <see cref="Gah.Blocks.CqrsEs.Events.IEvent" />
        /// </summary>
        /// <seealso cref="Gah.Blocks.CqrsEs.Events.IEvent" />
        private class EventStub : BasicEvent
        {
        }
    }
}
