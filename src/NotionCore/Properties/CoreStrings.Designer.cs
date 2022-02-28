using System;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace NotionCore.Properties;

public static class CoreStrings
{
    private static readonly ResourceManager _resourceManager
        = new ResourceManager("NotionCore.Properties.CoreStrings", typeof(CoreStrings).Assembly);
    
    public static string DbContextMissingConstructor(object contextType)
        => string.Format(
            GetString("DbContextMissingConstructor", nameof(contextType)),
            contextType);
    
    private static string GetString(string name, params string[] formatterNames)
    {
        var value = _resourceManager.GetString(name)!;
        for (var i = 0; i < formatterNames.Length; i++)
        {
            value = value.Replace("{" + formatterNames[i] + "}", "{" + i + "}");
        }

        return value;
    }
}