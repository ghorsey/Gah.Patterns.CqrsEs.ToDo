namespace Gah.Patterns.ToDo.Repository.Sql.Configuration
{
    using Gah.Patterns.ToDo.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Class <c>ToDoListConfiguration</c>.
    /// Implements the <see cref="IEntityTypeConfiguration{ToDoList}" />
    /// </summary>
    /// <seealso cref="IEntityTypeConfiguration{ToDoList}" />
    internal class ToDoListConfiguration : IEntityTypeConfiguration<ToDoList>
    {
        /// <summary>
        /// Configures the entity of type <see cref="ToDoList" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<ToDoList> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Title).HasMaxLength(500).IsRequired();
            builder.HasIndex(_ => _.Title);
            builder.Property(_ => _.Created).IsRequired();
            builder.Property(_ => _.TotalCompleted).IsRequired();
            builder.Property(_ => _.TotalItems).IsRequired();
            builder.Property(_ => _.TotalPending).IsRequired();
            builder.Property(_ => _.Updated).IsRequired();
        }
    }
}
