namespace HareDu.Extensions;

using System.Collections.Generic;
using System.Linq;
using Model;

public static class ValueExtensions
{
    internal static IDictionary<string, object> GetArgumentsOrNull(this IDictionary<string, ArgumentValue<object>> arguments)
    {
        if (arguments is null || !arguments.Any())
            return null;
            
        return arguments.ToDictionary(x => x.Key, x => x.Value.Value);
    }

    internal static IDictionary<string, object> GetArgumentsOrEmpty(this IDictionary<string, ArgumentValue<object>> arguments)
    {
        if (arguments is null || !arguments.Any())
            return new Dictionary<string, object>();
            
        return arguments.ToDictionary(x => x.Key, x => x.Value.Value);
    }

    internal static IDictionary<string, string> GetStringArguments(this IDictionary<string, ArgumentValue<object>> arguments)
    {
        if (arguments is null || !arguments.Any())
            return new Dictionary<string, string>();
            
        return arguments.ToDictionary(x => x.Key, x => x.Value.Value.ToString());
    }

    internal static IDictionary<string, ulong> GetArgumentsOrNull(this IDictionary<string, ArgumentValue<ulong>> arguments)
    {
        if (arguments is null || !arguments.Any())
            return new Dictionary<string, ulong>();
            
        return arguments.ToDictionary(x => x.Key, x => x.Value.Value);
    }
}