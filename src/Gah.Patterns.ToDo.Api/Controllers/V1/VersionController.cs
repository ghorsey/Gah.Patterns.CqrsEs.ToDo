namespace Gah.Patterns.ToDo.Api.Controllers.V1
{
    using System.Net;

    using Gah.Patterns.ToDo.Domain;
    using Gah.Patterns.ToDo.Domain.Query;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class VersionController.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class VersionController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VersionController" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public VersionController(ILogger<VersionController> logger)
            : base(logger)
        {
        }

        /// <summary>
        /// Versions this instance.
        /// </summary>
        /// <returns>An IActionResult.</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(Result<string>), (int)HttpStatusCode.OK)]
        public IActionResult Version()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var version = assembly.GetName().Version;

            this.Logger.LogInformation(1, "Fetching version: {versionString}", version.ToString());

            return this.Ok(version.ToString().MakeSuccessfulResult());
        }
    }
}
