namespace NotionCore.Infrastructure;

public interface INotionContextOptionsBuilderInfrastructure
{
    void AddOrUpdateExtension<TExtension>(TExtension extension)
        where TExtension : class, INotionContextOptionsExtension;
}