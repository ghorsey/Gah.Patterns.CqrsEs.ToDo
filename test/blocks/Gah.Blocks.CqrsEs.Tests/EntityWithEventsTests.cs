namespace Gah.Blocks.CqrsEs.Tests
{
    using System;

    using Gah.Blocks.CqrsEs.Events;

    using Xunit;

    /// <summary>
    /// A sample Unit test
    /// </summary>
    public class EntityWithEventsTests
    {
        /// <summary>
        /// Same Test1 Method
        /// </summary>
        [Fact]
        public void CreateEntityWithEventsTest()
        {
            var id = Guid.NewGuid();

            var e = new Stub(id);

            Assert.Equal(id, e.Id);
            Assert.NotNull(e.Events);
            Assert.Empty(e.Events);
        }

        /// <summary>
        /// Class Stub.
        /// Implements the <see cref="AggregateWithEvents{TId}" />
        /// Implements the <see cref="Gah.Blocks.CqrsEs.AggregateWithEvents{Guid}" />
        /// </summary>
        /// <seealso cref="Gah.Blocks.CqrsEs.AggregateWithEvents{Guid}" />
        private class Stub : AggregateWithEvents<Guid>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Stub" /> class.
            /// </summary>
            /// <param name="id">The identifier.</param>
            public Stub(Guid id)
                : base(id)
            {
            }
        }
    }
}
