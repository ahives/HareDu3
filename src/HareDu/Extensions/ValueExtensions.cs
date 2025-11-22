namespace HareDu.Extensions;

using System.Collections.Generic;
using System.Linq;
using Model;

public static class ValueExtensions
{
    extension(IDictionary<string, ArgumentValue<object>> arguments)
    {
        internal IDictionary<string, object> GetArgumentsOrNull()
        {
            if (arguments is null || !arguments.Any())
                return null;

            return arguments.ToDictionary(x => x.Key, x => x.Value.Value);
        }

        internal IDictionary<string, object> GetArgumentsOrEmpty()
        {
            if (arguments is null || !arguments.Any())
                return new Dictionary<string, object>();

            return arguments.ToDictionary(x => x.Key, x => x.Value.Value);
        }

        internal IDictionary<string, string> GetStringArguments()
        {
            if (arguments is null || !arguments.Any())
                return new Dictionary<string, string>();

            return arguments.ToDictionary(x => x.Key, x => x.Value.Value.ToString());
        }
    }

    extension(IDictionary<string, ArgumentValue<ulong>> arguments)
    {
        internal IDictionary<string, ulong> GetArgumentsOrNull()
        {
            if (arguments is null || !arguments.Any())
                return new Dictionary<string, ulong>();

            return arguments.ToDictionary(x => x.Key, x => x.Value.Value);
        }
    }
}