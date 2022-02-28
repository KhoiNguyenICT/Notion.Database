namespace NotionCore.Infrastructure;

public abstract class NotionContextOptionsExtensionInfo
{
    protected NotionContextOptionsExtensionInfo(INotionContextOptionsExtension extension)
    {
        Extension = extension;
    }

    public virtual INotionContextOptionsExtension Extension { get; }

    public abstract bool IsDatabaseProvider { get; }

    public abstract string LogFragment { get; }

    public abstract int GetServiceProviderHashCode();

    public abstract bool ShouldUseSameServiceProvider(NotionContextOptionsExtensionInfo other);

    public abstract void PopulateDebugInfo(IDictionary<string, string> debugInfo);
}