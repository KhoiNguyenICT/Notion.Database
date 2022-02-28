namespace NotionCore.Infrastructure.Internal;

public sealed class TypeFullNameComparer : IComparer<Type>, IEqualityComparer<Type>
{
    private TypeFullNameComparer()
    {
    }

    public static readonly TypeFullNameComparer Instance = new();

    public int Compare(Type? x, Type? y)
    {
        if (ReferenceEquals(x, y))
        {
            return 0;
        }

        if (x == null)
        {
            return -1;
        }

        return y == null ? 1 : StringComparer.Ordinal.Compare(x.FullName, y.FullName);
    }

    public bool Equals(Type? x, Type? y)
        => Compare(x, y) == 0;

    public int GetHashCode(Type obj)
        => obj.Name.GetHashCode();
}