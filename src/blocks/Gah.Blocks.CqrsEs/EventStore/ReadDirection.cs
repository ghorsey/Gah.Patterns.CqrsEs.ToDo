namespace Gah.Blocks.CqrsEs.EventStore
{
    /// <summary>
    /// Enum ReadDirection
    /// </summary>
    public enum ReadDirection
    {
        /// <summary>
        /// The stream was read forward
        /// </summary>
        Forward,

        /// <summary>
        /// The stream was read backward
        /// </summary>
        Backward
    }
}