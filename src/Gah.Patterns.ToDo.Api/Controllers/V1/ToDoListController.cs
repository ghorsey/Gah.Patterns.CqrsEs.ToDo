namespace Gah.Patterns.ToDo.Api.Controllers.V1
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Gah.Blocks.CqrsEs.Commands;
    using Gah.Blocks.CqrsEs.Queries;

    using Gah.Patterns.ToDo.Api.Models.Queries.Lists;
    using Gah.Patterns.ToDo.Api.Models.ToDoList;
    using Gah.Patterns.ToDo.Commands.Domain.Commands.Lists;
    using Gah.Patterns.ToDo.Query.Domain;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class <c>ToListController</c>.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}")]
    public class ToDoListController : Controller
    {
        /// <summary>
        /// The query bus
        /// </summary>
        private readonly IQueryBus queryBus;

        /// <summary>
        /// The command bus
        /// </summary>
        private readonly ICommandBus commandBus;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoListController"/> class.
        /// </summary>
        /// <param name="queryBus">The query bus.</param>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="logger">The logger.</param>
        public ToDoListController(IQueryBus queryBus, ICommandBus commandBus, ILogger<ToDoListController> logger)
        {
            this.queryBus = queryBus;
            this.commandBus = commandBus;
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
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A/an <c>IActionResult</c>.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Result<ToDoList>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            this.logger.LogDebug("Looking up a list by id {id}", id);

            var result = await this.queryBus.ExecuteAsync<FindListQuery, ToDoList>(new FindListQuery(id));

            return this.Ok(result.MakeSuccessfulResult());
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <returns>A/an <c>Task&lt;IActionResult&gt;</c>.</returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(Result<ToDoList>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create(ListInfo info)
        {
            this.logger.LogDebug("Got request to create event titled {@eventInfo}", info);
            var createCommand = new CreateListCommand(Guid.NewGuid(), info.Title);

            await this.commandBus.ExecuteAsync(createCommand);
            var list =
                await this.queryBus.ExecuteAsync<FindListQuery, ToDoList>(
                    new FindListQuery(createCommand.Id));

            return this.CreatedAtAction(
                nameof(this.GetAsync),
                new { id = createCommand.Id },
                list.MakeSuccessfulResult());
        }

        /// <summary>
        /// Update the specified list id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="info">The information.</param>
        /// <returns>A/an <c>Task&lt;IActionResult&gt;</c>.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Result<ToDoList>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Updated(Guid id, ListInfo info)
        {
            this.logger.LogDebug(
                "Got request to update the event {id} with the title {title}",
                id,
                info.Title);
            var updateListCommand = new UpdateListCommand(id, info.Title);

            await this.commandBus.ExecuteAsync(updateListCommand);
            return await this.GetAsync(id);
        }
    }
}