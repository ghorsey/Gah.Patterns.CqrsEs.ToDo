namespace Gah.Patterns.Todo.Repository.Sql.Data
{
    using Gah.Patterns.Todo.Domain;
    using Gah.Patterns.Todo.Repository.Sql.Configuration;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Class <c>ApplicationDbContext</c>.
    /// Implements the <see cref="Microsoft.EntityFrameworkCore.DbContext" />
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets to do lists.
        /// </summary>
        /// <value>To do lists.</value>
        public DbSet<ToDoList> ToDoLists { get; protected set; }

        /// <summary>
        /// Gets or sets to do items.
        /// </summary>
        /// <value>To do items.</value>
        public DbSet<ToDoItem> ToDoItems { get; protected set; }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.</remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ToDoListConfiguration());
            modelBuilder.ApplyConfiguration(new ToDoItemConfiguration());
        }
    }
}
