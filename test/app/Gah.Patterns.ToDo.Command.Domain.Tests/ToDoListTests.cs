namespace Gah.Patterns.ToDo.Command.Domain.Tests
{
    using System;

    using Gah.Blocks.CqrsEs;
    using Gah.Patterns.ToDo.Command.Domain.Events;
    using Gah.Patterns.ToDo.Command.Domain.Events.Lists;

    using Xunit;

    /// <summary>
    /// A sample Unit test
    /// </summary>
    public class ToDoListTests
    {
        /// <summary>
        /// Same Test1 Method
        /// </summary>
        [Fact]
        public void ApplyEventsTests()
        {
            var evt = new ListCreatedEvent(
                Guid.NewGuid(),
                "Test Title",
                DateTime.UtcNow,
                DateTime.UtcNow);

            var list = new ToDoList(new[] { evt });

            Assert.Equal(evt.Id, list.Id);
            Assert.Equal(evt.Title, list.Title);
            Assert.Equal(evt.Created, list.Created);
            Assert.Equal(evt.Updated, list.Updated);
        }
    }
}
