using NotionCore.Infrastructure;

namespace NotionCore;

public class NotionContext : IInfrastructure<IServiceProvider>
{
    IServiceProvider IInfrastructure<IServiceProvider>.Instance => default!;
}