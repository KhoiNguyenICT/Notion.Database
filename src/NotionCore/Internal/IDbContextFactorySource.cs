namespace NotionCore.Internal;

public interface IDbContextFactorySource<TContext>
    where TContext : NotionContext
{
    Func<IServiceProvider, NotionContextOptions<TContext>, TContext> Factory { get; }
}