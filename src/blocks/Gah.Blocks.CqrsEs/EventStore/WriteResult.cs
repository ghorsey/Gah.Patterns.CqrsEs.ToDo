namespace Gah.Blocks.CqrsEs.EventStore
{
    /// <summary>
    /// Struct WriteResult
    /// </summary>
    public struct WriteResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WriteResult"/> struct.
        /// </summary>
        /// <param name="nextExpectedVersion">The next expected version.</param>
        public WriteResult(long nextExpectedVersion)
        {
            this.NextExpectedVersion = nextExpectedVersion;
        }

        /// <summary>
        /// Gets the next expected version.
        /// </summary>
        /// <value>The next expected version.</value>
        public long NextExpectedVersion { get; }
    }
}