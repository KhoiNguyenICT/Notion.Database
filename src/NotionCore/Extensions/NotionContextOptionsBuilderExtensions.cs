using NotionCore.Infrastructure.Internal;

namespace NotionCore.Extensions;

public static class NotionContextOptionsBuilderExtensions
{
    public static NotionContextOptionsBuilder UseNotion(
        this NotionContextOptionsBuilder optionsBuilder,
        string connectionString)
    {
        ConfigureWarnings(optionsBuilder);

        return optionsBuilder;
    }

    public static NotionContextOptionsBuilder<TContext> UseNotion<TContext>(
        this NotionContextOptionsBuilder<TContext> optionsBuilder,
        string connectionString)
        where TContext : NotionContext
        => (NotionContextOptionsBuilder<TContext>) UseNotion(
            (NotionContextOptionsBuilder) optionsBuilder, connectionString);

    private static NotionOptionsExtension GetOrCreateExtension(NotionContextOptionsBuilder optionsBuilder)
        => optionsBuilder.Options.FindExtension<NotionOptionsExtension>()
           ?? new NotionOptionsExtension();

    private static void ConfigureWarnings(NotionContextOptionsBuilder optionsBuilder)
    {
    }
}