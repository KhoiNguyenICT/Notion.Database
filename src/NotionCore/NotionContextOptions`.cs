namespace NotionCore;

public class NotionContextOptions<TNotionContext> : NotionContextOptions
    where TNotionContext : NotionContext
{
    public NotionContextOptions()
    {
    }

    public override NotionContextOptions WithExtension<TExtension>(TExtension extension)
    {
        return default!;
    }
}