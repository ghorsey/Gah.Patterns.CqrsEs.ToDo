namespace Gah.Blocks.CqrsEs.Tests.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using Gah.Blocks.CqrsEs.Commands;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using Moq;

    using Xunit;

    /// <summary>
    /// Class CommandBusTests.
    /// </summary>
    public class CommandBusTests
    {
        /// <summary>
        /// Defines the test method ExecuteAsyncMethodTest.
        /// </summary>
        /// <returns>A/an <c>Task.</c></returns>
        [Fact]
        public async Task ExecuteAsyncMethodTest()
        {
            var cmdMock = new Mock<ICommand>(MockBehavior.Strict);
            var cancellationToken = new CancellationToken(false);

            var mediatorMock = new Mock<IMediator>(MockBehavior.Strict);
            mediatorMock.Setup(_ => _.Send(cmdMock.Object, cancellationToken)).ReturnsAsync(Unit.Value);

            var loggerMock = new Mock<ILogger<CommandBus>>(MockBehavior.Loose);

            var bus = new CommandBus(mediatorMock.Object, loggerMock.Object);

            await bus.ExecuteAsync(cmdMock.Object, cancellationToken);

            mediatorMock.Verify(_ => _.Send(cmdMock.Object, cancellationToken), Times.Once);
        }
    }
}
