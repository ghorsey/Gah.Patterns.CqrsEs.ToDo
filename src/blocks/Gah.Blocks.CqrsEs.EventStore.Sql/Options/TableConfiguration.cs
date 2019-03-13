namespace Gah.Blocks.CqrsEs.EventStore.Sql.Options
{
    /// <summary>
    /// Class <c>TableConfiguration</c>.
    /// </summary>
    public class TableConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableConfiguration"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TableConfiguration(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableConfiguration"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="schema">The schema.</param>
        public TableConfiguration(string name, string schema)
            : this(name)
        {
            this.Schema = schema;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the schema.
        /// </summary>
        /// <value>The schema.</value>
        public string Schema { get; set; }
    }
}