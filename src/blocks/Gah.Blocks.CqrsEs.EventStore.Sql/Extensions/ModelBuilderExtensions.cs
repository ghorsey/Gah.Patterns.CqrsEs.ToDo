namespace Gah.Blocks.CqrsEs.EventStore.Sql.Extensions
{
    using Gah.Blocks.CqrsEs.EventStore.Sql.Entities;
    using Gah.Blocks.CqrsEs.EventStore.Sql.Options;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Class <c>ModelBuilderExtensions</c>.
    /// </summary>
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Configures the event source context.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="options">The options.</param>
        public static void ConfigureEventSourceContext(
            this ModelBuilder builder,
            EventStoreOptions options)
        {
            if (!string.IsNullOrWhiteSpace(options.DefaultSchema))
            {
                builder.HasDefaultSchema(options.DefaultSchema);
            }

            builder.Entity<EventSource>(
                es =>
                    {
                        es.ToTable(options.EventStore);

                        es.HasKey(o => o.Id);

                        es.Property(o => o.EventNumber).IsRequired();
                        es.Property(o => o.Stream).HasMaxLength(2000).IsRequired();
                        es.Property(o => o.Event).IsRequired();
                        es.Property(o => o.EventType).HasMaxLength(2000).IsRequired();

                        es.HasAlternateKey(
                            nameof(EventSource.Stream),
                            nameof(EventSource.EventNumber));
                    });
        }

        /// <summary>
        /// To the table.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="entityTypeBuilder">The entity type builder.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>A/an <c>EntityTypeBuilder&lt;TEntity&gt;</c>.</returns>
        private static EntityTypeBuilder<TEntity> ToTable<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, TableConfiguration configuration)
            where TEntity : class
        {
            return string.IsNullOrWhiteSpace(configuration.Schema) ? entityTypeBuilder.ToTable(configuration.Name) : entityTypeBuilder.ToTable(configuration.Name, configuration.Schema);
        }
    }
}
