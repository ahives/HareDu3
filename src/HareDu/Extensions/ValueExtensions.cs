namespace HareDu.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Model;

    public static class ValueExtensions
    {
        public static IDictionary<string, object> GetArguments(this IDictionary<string, ArgumentValue<object>> arguments) => arguments.ToDictionary(x => x.Key, x => x.Value.Value);
    }
}