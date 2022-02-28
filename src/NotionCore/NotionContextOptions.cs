using System.Collections.Immutable;
using NotionCore.Infrastructure;
using NotionCore.Infrastructure.Internal;

namespace NotionCore;

public abstract class NotionContextOptions : INotionContextOptions
{
    private readonly ImmutableSortedDictionary<Type, (INotionContextOptionsExtension Extension, int Ordinal)>
        _extensionsMap;

    protected NotionContextOptions()
    {
        _extensionsMap =
            ImmutableSortedDictionary.Create<Type, (INotionContextOptionsExtension, int)>(TypeFullNameComparer.Instance);
    }

    public abstract NotionContextOptions WithExtension<TExtension>(TExtension extension)
        where TExtension : class, INotionContextOptionsExtension;

    public virtual TExtension? FindExtension<TExtension>()
        where TExtension : class, INotionContextOptionsExtension
        => _extensionsMap.TryGetValue(typeof(TExtension), out var value) ? (TExtension) value.Extension : null;
}