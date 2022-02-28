using Humanizer;

namespace NotionCore.Infrastructure;

public static class TypeExtensions
{
    public static string ShortDisplayName(this Type type)
        => type.Name.Pluralize();
}