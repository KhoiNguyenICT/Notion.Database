using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace NotionCore.Infrastructure;

public class CoreOptionsExtension : INotionContextOptionsExtension
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
        private int? _serviceProviderHash;
        private string? _logFragment;

        public ExtensionInfo(CoreOptionsExtension extension)
            : base(extension)
        {
        }

        private new CoreOptionsExtension Extension
            => (CoreOptionsExtension) base.Extension;

        public override bool IsDatabaseProvider
            => false;

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
            if (_serviceProviderHash != null) return _serviceProviderHash.Value;
            var hashCode = new HashCode();
            _serviceProviderHash = hashCode.ToHashCode();

            return _serviceProviderHash.Value;
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