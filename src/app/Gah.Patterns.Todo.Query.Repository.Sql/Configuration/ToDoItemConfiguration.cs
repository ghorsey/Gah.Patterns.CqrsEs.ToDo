namespace Gah.Patterns.ToDo.Query.Repository.Sql.Configuration
{
    using Gah.Patterns.ToDo.Query.Domain;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// The to do item configuration.
    /// </summary>
    public class ToDoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
    {
        /// <summary>
        /// Configures the entity of type <see cref="ToDoItem" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<ToDoItem> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.ListId).IsRequired();
            builder.Property(_ => _.Title).HasMaxLength(500).IsRequired();
            builder.Property(_ => _.Created).IsRequired();
            builder.Property(_ => _.IsDone).IsRequired();
            builder.Property(_ => _.Updated).IsRequired();
        }
    }
}