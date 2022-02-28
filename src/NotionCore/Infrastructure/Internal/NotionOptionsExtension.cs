using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace NotionCore.Infrastructure.Internal;

public class NotionOptionsExtension : INotionContextOptionsExtension
{
    private NotionContextOptionsExtensionInfo? _info;

    public virtual NotionContextOptionsExtensionInfo Info
        => _info ??= new ExtensionInfo(this);

    public void ApplyServices(IServiceCollection services)
    {
        throw new NotImplementedException();
    }

    public void Validate(INotionContextOptions options)
    {
        throw new NotImplementedException();
    }

    private sealed class ExtensionInfo : NotionContextOptionsExtensionInfo
    {
        private string? _logFragment;

        public ExtensionInfo(INotionContextOptionsExtension extension) : base(extension)
        {
        }

        public override bool IsDatabaseProvider => false;

        public override string LogFragment
        {
            get
            {
                if (_logFragment != null) return _logFragment;
                var builder = new StringBuilder();
                _logFragment = builder.ToString();

                return _logFragment;
            }
        }

        public override int GetServiceProviderHashCode()
        {
            throw new NotImplementedException();
        }

        public override bool ShouldUseSameServiceProvider(NotionContextOptionsExtensionInfo other)
        {
            throw new NotImplementedException();
        }

        public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
        {
            throw new NotImplementedException();
        }
    }
}