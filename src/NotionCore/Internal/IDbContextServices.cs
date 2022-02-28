namespace NotionCore.Internal;

public interface IDbContextServices
{
    IDbContextServices Initialize(
        IServiceProvider scopedProvider,
        NotionContextOptions contextOptions,
        NotionContext context);
}