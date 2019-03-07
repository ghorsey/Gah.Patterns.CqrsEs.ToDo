namespace Gah.Patterns.ToDo.Repository.Sql.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Gah.Patterns.Todo.Domain;
    using Gah.Patterns.Todo.Repository.Sql;

    using Microsoft.Extensions.Logging;

    using Moq;

    using Xunit;

    /// <summary>
    /// Class <c>ToDoListRepositoryTests</c>.
    /// </summary>
    public class ToDoListRepositoryTests : SqliteTests
    {
        /// <summary>
        /// The lists
        /// </summary>
        private readonly List<ToDoList> lists = new List<ToDoList>
                                                    {
                                                        new ToDoList(
                                                            Guid.NewGuid(),
                                                            "Initial List",
                                                            0,
                                                            0,
                                                            0,
                                                            DateTime.UtcNow,
                                                            DateTime.UtcNow),
                                                        new ToDoList(
                                                            Guid.NewGuid(),
                                                            "Second List 2",
                                                            0,
                                                            0,
                                                            0,
                                                            DateTime.UtcNow,
                                                            DateTime.UtcNow)
                                                    };

        /// <summary>
        /// Same Test1 Method
        /// </summary>
        /// <returns>A/an <c>Task</c>.</returns>
        [Fact]
        public async Task CreateListAsync()
        {
            var loggerMock = new Mock<ILogger<ToDoListRepository>>(MockBehavior.Loose);

            await this.RunTestAsync(
                async (context) =>
                    {
                        var repo = new ToDoListRepository(context, loggerMock.Object);

                        await repo.CreateListAsync(this.lists[0]);

                        var result = await repo.FindListAsync(this.lists[0].Id);
                        Assert.NotNull(result);

                        Assert.Equal(this.lists[0].Title, result.Title);
                        Assert.Equal(this.lists[0].Id, result.Id);
                        Assert.Equal(this.lists[0].Created, result.Created);
                        Assert.Equal(this.lists[0].TotalCompleted, result.TotalCompleted);
                        Assert.Equal(this.lists[0].TotalItems, result.TotalItems);
                        Assert.Equal(this.lists[0].Updated, result.Updated);
                    });
        }

        /// <summary>
        /// Same Test1 Method
        /// </summary>
        /// <returns>A/an <c>Task</c>.</returns>
        [Fact]
        public async Task FetchListTestAsync()
        {
            var loggerMock = new Mock<ILogger<ToDoListRepository>>(MockBehavior.Loose);

            await this.RunTestAsync(
                async (context) =>
                    {
                        var repo = new ToDoListRepository(context, loggerMock.Object);

                        await repo.CreateListAsync(this.lists[0]);
                        await repo.CreateListAsync(this.lists[1]);

                        var result = await repo.FindListsAsync();
                        var result2 = await repo.FindListsAsync("Second");
                        Assert.NotNull(result);
                        Assert.Equal(2, result.Count);

                        Assert.Equal(this.lists[0].Title, result[0].Title);
                        Assert.Equal(this.lists[0].Id, result[0].Id);
                        Assert.Equal(this.lists[0].Created, result[0].Created);
                        Assert.Equal(this.lists[0].TotalCompleted, result[0].TotalCompleted);
                        Assert.Equal(this.lists[0].TotalItems, result[0].TotalItems);
                        Assert.Equal(this.lists[0].Updated, result[0].Updated);

                        Assert.NotNull(result2);
                        Assert.Single(result2);
                        Assert.Equal(this.lists[1].Id, result2[0].Id);
                    });
        }

        /// <summary>
        /// update list as an asynchronous operation.
        /// </summary>
        /// <returns>A/an <c>Task</c>.</returns>
        [Fact]
        public async Task UpdateListAsync()
        {
            var loggerMock = new Mock<ILogger<ToDoListRepository>>(MockBehavior.Loose);

            await this.RunTestAsync(
                async (context) =>
                    {
                        var repo = new ToDoListRepository(context, loggerMock.Object);

                        await repo.CreateListAsync(this.lists[0]);

                        var updated = new ToDoList(
                            this.lists[0].Id,
                            "New Name",
                            0,
                            0,
                            0,
                            DateTime.UtcNow,
                            this.lists[0].Created);

                        await repo.UpdateListAsync(updated);

                        var result = await repo.FindListAsync(this.lists[0].Id);
                        Assert.NotNull(result);

                        Assert.Equal(result.Title, updated.Title);
                        Assert.Equal(this.lists[0].Id, result.Id);
                    });
        }
    }
}
