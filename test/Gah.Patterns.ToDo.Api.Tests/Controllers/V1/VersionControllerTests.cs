namespace Gah.Patterns.ToDo.Api.Tests.Controllers.V1
{
    // ReSharper disable once StyleCop.SA1210
    using Gah.Patterns.ToDo.Api.Controllers.V1;
    using Gah.Patterns.ToDo.Query.Domain;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using Moq;

    using Xunit;

    /// <summary>
    /// Class <c>VersionControllerTests</c>.
    /// </summary>
    public class VersionControllerTests
    {
        /// <summary>
        /// Defines the test method TestGetMethod.
        /// </summary>
        [Fact]
        public void TestGetMethod()
        {
            var version = typeof(VersionController).Assembly.GetName().Version;
            var expectedVersion = version.ToString();

            var loggerMock = new Mock<ILogger<VersionController>>();
            var controller = new VersionController(loggerMock.Object);

            var response = controller.Version() as OkObjectResult;

            Assert.NotNull(response);

            var result = response.Value as Result<string>;

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(expectedVersion, result.Value);
        }
    }
}
