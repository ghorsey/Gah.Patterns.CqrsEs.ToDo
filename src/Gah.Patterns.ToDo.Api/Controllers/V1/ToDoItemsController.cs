namespace Gah.Patterns.ToDo.Api.Controllers.V1
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    using Gah.Blocks.CqrsEs.Commands;
    using Gah.Blocks.CqrsEs.Queries;
    using Gah.Patterns.ToDo.Api.Models.Queries.Items;
    using Gah.Patterns.ToDo.Api.Models.ToDoItem;
    using Gah.Patterns.ToDo.Command.Domain.Commands.Items;
    using Gah.Patterns.ToDo.Query.Domain;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Class <c>ToDoItemsController</c>.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/{listId}/items")]
    public class ToDoItemsController : Controller
    {
        /// <summary>
        /// The command bus
        /// </summary>
        private readonly ICommandBus commandBus;

        /// <summary>
        /// The query bus
        /// </summary>
        private readonly IQueryBus queryBus;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoItemsController"/> class.
        /// </summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="queryBus">The query bus.</param>
        /// <param name="logger">The logger.</param>
        public ToDoItemsController(ICommandBus commandBus, IQueryBus queryBus, ILogger<ToDoItemsController> logger)
        {
            this.commandBus = commandBus;
            this.queryBus = queryBus;
            this.logger = logger;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="listId">The list identifier.</param>
        /// <returns>A/an <c>Task&lt;IActionResult&gt;</c>.</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(Result<List<ToDoItem>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAsync(Guid listId)
        {
            this.logger.LogDebug("Fetching all the items for list {listId}", listId);

            var result = await this.queryBus.ExecuteAsync<FindAllItemsQuery, List<ToDoItem>>(
                new FindAllItemsQuery(listId));

            return this.Ok(result.MakeSuccessfulResult());
        }

        /// <summary>
        /// get as an asynchronous operation.
        /// </summary>
        /// <param name="listId">The list identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>A/an <c>Task&lt;IActionResult&gt;</c>.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Result<ToDoItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync(Guid listId, Guid id)
        {
            this.logger.LogDebug("Fetching the item {id} from the list {listId}", id, listId);

            var result =
                await this.queryBus.ExecuteAsync<FindItemQuery, ToDoItem>(new FindItemQuery(id));

            return this.Ok(result.MakeSuccessfulResult());
        }

        /// <summary>
        /// create as an asynchronous operation.
        /// </summary>
        /// <param name="listId">The list identifier.</param>
        /// <param name="info">The information.</param>
        /// <returns>A/an <c>Task&lt;IActionResult&gt;</c>.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Result<ToDoItem>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAsync(Guid listId, ItemInfo info)
        {
            this.logger.LogDebug("Attempting to create list item {title}", info.Title);

            var command = new CreateItemCommand(Guid.NewGuid(), listId, info.Title);
            await this.commandBus.ExecuteAsync(command);

            var result =
                await this.queryBus.ExecuteAsync<FindItemQuery, ToDoItem>(new FindItemQuery(command.Id));

            return this.CreatedAtAction(
                nameof(this.GetAsync),
                new { listId, id = command.Id },
                result.MakeSuccessfulResult());
        }

        /// <summary>
        /// update as an asynchronous operation.
        /// </summary>
        /// <param name="listId">The list identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="info">The information.</param>
        /// <returns>A/an <c>Task&lt;IActionResult&gt;</c>.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Result<ToDoItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAsync(Guid listId, Guid id, ItemInfo info)
        {
            this.logger.LogDebug(
                "Attempting to update the item id {id} to with the following info {@info} for list {listId}",
                id,
                info,
                listId);

            var updateItemCommand = new UpdateItemCommand(id, listId, info.Title);

            await this.commandBus.ExecuteAsync(updateItemCommand);

            return await this.GetAsync(listId, id);
        }

        /// <summary>
        /// update is done as an asynchronous operation.
        /// </summary>
        /// <param name="listId">The list identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="info">The information.</param>
        /// <returns>A/an <c>Task&lt;IActionResult&gt;</c>.</returns>
        [HttpPost("{id}/is-done")]
        [ProducesResponseType(typeof(Result<ToDoItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateIsDoneAsync(Guid listId, Guid id, IsDoneInfo info)
        {
            this.logger.LogDebug(
                "Attempting to update the is done status of the item {id} with {@event}",
                id,
                info);

            var updateIsDoneCommand = new UpdateItemIsDoneCommand(id, listId, info.IsDone);

            await this.commandBus.ExecuteAsync(updateIsDoneCommand);

            return await this.GetAsync(listId, id);
        }
    }
}