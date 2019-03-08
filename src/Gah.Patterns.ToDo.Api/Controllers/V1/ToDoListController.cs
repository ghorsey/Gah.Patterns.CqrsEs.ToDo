namespace Gah.Patterns.ToDo.Api.Controllers.V1
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    using Gah.Blocks.CqrsEs.Queries;

    using Gah.Patterns.ToDo.Api.Models.Queries.Lists;
    using Gah.Patterns.ToDo.Query.Domain;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class <c>ToListController</c>.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}")]
    public class ToDoListController : ControllerBase
    {
        /// <summary>
        /// The query bus
        /// </summary>
        private readonly IQueryBus queryBus;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoListController" /> class.
        /// </summary>
        /// <param name="queryBus">The query bus.</param>
        /// <param name="logger">The logger.</param>
        public ToDoListController(IQueryBus queryBus, ILogger<ToDoListController> logger)
        {
            this.queryBus = queryBus;
            this.logger = logger;
        }

        /// <summary>
        /// Gets all lists in the system filtered by title (when present)
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns>A/an <c>Task&lt;Result&lt;List&lt;ToDoList&gt;&gt;&gt;</c>.</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(Result<List<ToDoList>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(string title)
        {
            this.logger.LogDebug("Looking up lists with title: '{title}'", title);
            var result =
                await this.queryBus.ExecuteAsync<FindAllListsQuery, List<ToDoList>>(
                    new FindAllListsQuery(title));

            return this.Ok(result.MakeSuccessfulResult());
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>A/an <c>Task&lt;IActionResult&gt;</c>.</returns>
        [HttpPost("")]
        public Task<IActionResult> Create()
        {
            throw new NotImplementedException();
        }
    }
}