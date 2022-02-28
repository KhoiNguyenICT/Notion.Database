using NotionCore.Infrastructure;

namespace NotionCore;

public class NotionContextOptionsBuilder : INotionContextOptionsBuilderInfrastructure
{
    private NotionContextOptions _options;

    public NotionContextOptionsBuilder()
        : this(new NotionContextOptions<NotionContext>())
    {
    }

    public NotionContextOptionsBuilder(NotionContextOptions options)
    {
        _options = options;
    }

    public virtual NotionContextOptions Options
        => _options;

    void INotionContextOptionsBuilderInfrastructure.AddOrUpdateExtension<TExtension>(TExtension extension)
        => _options = _options.WithExtension(extension);
}