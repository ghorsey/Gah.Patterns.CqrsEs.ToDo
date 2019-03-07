namespace Gah.Blocks.CqrsEs.Tests.Queries
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Gah.Blocks.CqrsEs.Queries;
    using MediatR;

    using Microsoft.Extensions.Logging;

    using Moq;

    using Xunit;

    /// <summary>
    /// Class CommandBusTests.
    /// </summary>
    public class QueryBusTests
    {
        /// <summary>
        /// Defines the test method ExecuteAsyncMethodTest.
        /// </summary>
        /// <returns>A/an <c>Task.</c></returns>
        [Fact]
        public async Task ExecuteAsyncMethodTest()
        {
            var query = new QueryStub();
            var cancellationToken = new CancellationToken(false);

            var mediatorMock = new Mock<IMediator>(MockBehavior.Strict);
            mediatorMock.Setup(_ => _.Send(query, cancellationToken)).ReturnsAsync(new List<int>());

            var loggerMock = new Mock<ILogger<QueryBus>>(MockBehavior.Loose);

            var bus = new QueryBus(mediatorMock.Object, loggerMock.Object);

            var result = await bus.ExecuteAsync<QueryStub, List<int>>(query, cancellationToken);

            mediatorMock.Verify(_ => _.Send(query, cancellationToken), Times.Once);
        }

        private class QueryStub : IQuery<List<int>>
        {
        }
    }
}
