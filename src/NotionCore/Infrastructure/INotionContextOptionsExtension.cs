using Microsoft.Extensions.DependencyInjection;

namespace NotionCore.Infrastructure;

public interface INotionContextOptionsExtension
{
    NotionContextOptionsExtensionInfo Info { get; }

    void ApplyServices(IServiceCollection services);

    void Validate(INotionContextOptions options);
}